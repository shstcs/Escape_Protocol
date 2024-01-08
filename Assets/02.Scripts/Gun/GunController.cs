using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GunController : MonoBehaviour
{   
    [Header("Gun")]
    public Gun CurrentGun;

    [Header("GunHolders")]
    [SerializeField] private List<GameObject> _gunHolders;

    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    [Header("Scope")]
    [SerializeField] private GameObject _scope;

    [Header("Sound")]
    [SerializeField] private AudioClip _reloadSound;
    [SerializeField] private AudioClip _fineSightSound;
    [SerializeField] private AudioClip _noBulletSound;

    public bool IsFineSightMode { get; private set; }

    private PlayerAttack _playerAttack;

    private CinemachinePOV _pov;
    private AudioSource _audioSource;  // 발사 소리 재생기
    private RaycastHit _hitInfo;  // 총알의 충돌 정보
    private float _currentFireRate;
    private bool _isReload = false;
    private bool _isSniper = false;
    private float _originFOV;



    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _pov = _virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        _playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
    }

    private void Start()
    {
        IsFineSightMode = false;
    }


    void Update()
    {
        if(Time.timeScale != 0)
        {
            GunFireRateCalc();
            TryFire();
            TryReload();
            TryFineSight();
        }
    }

    private void GunFireRateCalc()
    {
        if (_currentFireRate > 0)
            _currentFireRate -= Time.deltaTime; 
    }

    private void TryFire() 
    {
        if (Input.GetButton("Fire1") && _currentFireRate <= 0)
        {
            Fire();
        }
    }

    private void Fire() 
    {
        if (!_isReload)
        {
            if (CurrentGun.CurrentBulletCount > 0)
                Shoot();
            else
            {
                PlaySE(_noBulletSound);
                CancelFineSight();
                StartCoroutine(COReload());
            }
        }
    }

    private void Shoot()
    {
        CurrentGun.CurrentBulletCount--;
        _currentFireRate = CurrentGun.FireRate;
        PlaySE(CurrentGun.Fire_Sound);
        if (PlayerPrefs.GetInt("GunEffects") == 1) CurrentGun.MuzzleFlash.Play();
        CurrentGun.Anim.SetTrigger("Fire");

        // 피격 처리
        Hit();

        // 총기 반동 코루틴 실행
        StopAllCoroutines();
        StartCoroutine(CORetroAction());

        Debug.Log("총알 발사");
    }

    private void TryReload()
    {
        if (Input.GetKeyDown(KeyCode.R) && !_isReload && CurrentGun.CurrentBulletCount < CurrentGun.ReloadBulletCount)
        {
            CancelFineSight();
            StartCoroutine(COReload());
        }
    }

    private void Hit()
    {
        Vector3 randomRange = new Vector3(Random.Range(-CurrentGun.Accuracy, CurrentGun.Accuracy), Random.Range(-CurrentGun.Accuracy, CurrentGun.Accuracy), 0);
        Vector3 randomRangeFineSight = new Vector3(Random.Range(-CurrentGun.AccuracyFineSight, CurrentGun.AccuracyFineSight), Random.Range(-CurrentGun.AccuracyFineSight, CurrentGun.AccuracyFineSight), 0);

        if (!IsFineSightMode)  // 정조준이 아닌 상태
        {
            Debug.DrawRay(Camera.main.transform.position, (Camera.main.transform.forward + randomRange) * CurrentGun.Range, Color.blue, 0.3f);
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward + randomRange, out _hitInfo, CurrentGun.Range)) // 카메라 월드좌표
            {
                GameObject clone = Instantiate(CurrentGun.HitEffectPrefab, _hitInfo.point, Quaternion.LookRotation(_hitInfo.normal));

                Debug.Log(_hitInfo.transform.name);
                if (_hitInfo.transform.name == "Monster")
                {
                    _playerAttack.Attack();
                    Debug.Log("총알이 몬스터한테 맞았네요");
                }
            }
        }
        else // 정조준 상태
        {
            Debug.DrawRay(Camera.main.transform.position, (Camera.main.transform.forward + randomRangeFineSight) * CurrentGun.Range, Color.blue, 0.3f);
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward + randomRangeFineSight, out _hitInfo, CurrentGun.Range)) // 카메라 월드좌표
            {
                GameObject clone = Instantiate(CurrentGun.HitEffectPrefab, _hitInfo.point, Quaternion.LookRotation(_hitInfo.normal));
                
                Debug.Log(_hitInfo.transform.name);
                if (_hitInfo.transform.name == "Monster")
                {
                    _playerAttack.Attack();
                    Debug.Log("총알이 몬스터한테 맞았어요");
                }
            }
        }

    }

    private void TryFineSight()
    {
        if (Input.GetButtonDown("Fire2") && !_isReload)
        {
            FineSight();
        }
    }

    private void FineSight()
    {
        IsFineSightMode = !IsFineSightMode;
        CurrentGun.Anim.SetBool("FineSightMode", IsFineSightMode);

        if (IsFineSightMode)
        {
            StopAllCoroutines();
            StartCoroutine(COFineSightActivate());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(COFineSightDeActivate());
        }
    }

    private void CancelFineSight()
    {
        if (IsFineSightMode)
            FineSight();
    }

    IEnumerator COFineSightActivate()
    {
        PlaySE(_fineSightSound);

        _originFOV = _virtualCamera.m_Lens.FieldOfView;

        if(!_isSniper)
        {
            while (CurrentGun.transform.localPosition != CurrentGun.FineSightOriginPos)
            {
                CurrentGun.transform.localPosition = Vector3.Lerp(CurrentGun.transform.localPosition, CurrentGun.FineSightOriginPos, 0.2f);
                _virtualCamera.m_Lens.FieldOfView = 30f;

                yield return null;
            }
        }
        else
        {
            while (CurrentGun.transform.localPosition != CurrentGun.FineSightOriginPos)
            {
                CurrentGun.transform.localPosition = Vector3.Lerp(CurrentGun.transform.localPosition, CurrentGun.FineSightOriginPos, 0.2f);
                _virtualCamera.m_Lens.FieldOfView = 20f;
                _scope.SetActive(true);

                yield return null;
            }
        }

    }

    IEnumerator COFineSightDeActivate()
    {
        if(!_isSniper)
        {
            while (CurrentGun.transform.localPosition != CurrentGun.OriginPos)
            {
                CurrentGun.transform.localPosition = Vector3.Lerp(CurrentGun.transform.localPosition, CurrentGun.OriginPos, 0.2f);
                _virtualCamera.m_Lens.FieldOfView = _originFOV;

                yield return null;
            }
        }
        else
        {
            while (CurrentGun.transform.localPosition != CurrentGun.OriginPos)
            {
                CurrentGun.transform.localPosition = Vector3.Lerp(CurrentGun.transform.localPosition, CurrentGun.OriginPos, 0.2f);
                _virtualCamera.m_Lens.FieldOfView = _originFOV;
                _scope.SetActive(false);

                yield return null;
            }
        }
    }

    IEnumerator COReload()
    {
        if (CurrentGun.CarryBulletCount > 0)
        {
            PlaySE(_reloadSound);

            _isReload = true;
            CurrentGun.Anim.SetTrigger("Reload");

            CurrentGun.CarryBulletCount += CurrentGun.CurrentBulletCount;
            CurrentGun.CurrentBulletCount = 0;

            yield return new WaitForSeconds(CurrentGun.ReloadTime);

            if (CurrentGun.CarryBulletCount >= CurrentGun.ReloadBulletCount)
            {
                CurrentGun.CurrentBulletCount = CurrentGun.ReloadBulletCount;
                CurrentGun.CarryBulletCount -= CurrentGun.ReloadBulletCount;
            }
            else
            {
                CurrentGun.CurrentBulletCount = CurrentGun.CarryBulletCount;
                CurrentGun.CarryBulletCount = 0;
            }

            _isReload = false;
        }
        else
        {
            PlaySE(_noBulletSound);
            Debug.Log("소유한 총알이 없습니다.");
        }
    }

    IEnumerator CORetroAction()
    {
        Vector3 recoilBack = new Vector3(CurrentGun.OriginPos.x, CurrentGun.OriginPos.y, CurrentGun.OriginPos.z - CurrentGun.RetroActionForce); // 정조준 x
        Vector3 retroActionRecoilBack = new Vector3(CurrentGun.FineSightOriginPos.x, CurrentGun.FineSightOriginPos.y, CurrentGun.FineSightOriginPos.z - CurrentGun.RetroActionFineSightForce);  // 정조준

        if (!IsFineSightMode)  // 정조준이 아닌 상태
        {
            CurrentGun.transform.localPosition = CurrentGun.OriginPos;

            // 반동 시작
            while (CurrentGun.transform.localPosition.z >= CurrentGun.OriginPos.z - CurrentGun.RetroActionForce + 0.02f)
            {
                CurrentGun.transform.localPosition = Vector3.Lerp(CurrentGun.transform.localPosition, recoilBack, 0.4f);
                _pov.m_VerticalAxis.Value += -CurrentGun.RetroActionForce;
                yield return null;
            }

            // 원위치
            while (CurrentGun.transform.localPosition != CurrentGun.OriginPos)
            {
                CurrentGun.transform.localPosition = Vector3.Lerp(CurrentGun.transform.localPosition, CurrentGun.OriginPos, 0.1f);
                yield return null;
            }
        }
        else  // 정조준 상태
        {
            CurrentGun.transform.localPosition = CurrentGun.FineSightOriginPos;

            // 반동 시작
            while (CurrentGun.transform.localPosition.z >= CurrentGun.FineSightOriginPos.z - CurrentGun.RetroActionFineSightForce + 0.02f)
            {
                CurrentGun.transform.localPosition = Vector3.Lerp(CurrentGun.transform.localPosition, retroActionRecoilBack, 0.4f);
                _pov.m_VerticalAxis.Value += -CurrentGun.RetroActionFineSightForce;
                yield return null;
            }

            // 원위치
            while (CurrentGun.transform.localPosition != CurrentGun.FineSightOriginPos)
            {
                CurrentGun.transform.localPosition = Vector3.Lerp(CurrentGun.transform.localPosition, CurrentGun.FineSightOriginPos, 0.1f);
                yield return null;
            }
        }
    }

    public void PlaySE(AudioClip _clip)
    {
        _audioSource.PlayOneShot(_clip);
    }

    public void EquipM4()
    {
        foreach(GameObject gun in _gunHolders)
        {
            gun.SetActive(false);
        }

        CurrentGun = _gunHolders[1].GetComponent<Gun>();
        _gunHolders[1].SetActive(true);
        _isSniper = false;

        Main.Game.CallWeaponGet();
    }

    public void EquipM82()
    {
        foreach (GameObject gun in _gunHolders)
        {
            gun.SetActive(false);
        }

        CurrentGun = _gunHolders[2].GetComponent<Gun>();
        _gunHolders[2].SetActive(true);
        _isSniper = true;

        Main.Game.CallWeaponGet();
    }
}