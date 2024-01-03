using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_HUDPanel : MonoBehaviour
{
    [Header("Condition")]
    [SerializeField] private Image _hpBar;
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private Image _staminaBar;
    [SerializeField] private TMP_Text _staminaText;
    [SerializeField] private TMP_Text _bulletText;
    [SerializeField] private Image _damagedbackground;
    [SerializeField] private TMP_Text _usedBullet;

    [Header("Quest")]
    [SerializeField] private TMP_Text _questText;

    private Player _player;
    private PlayerHealth _health;
    private Gun _gun;

    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _health = GameObject.Find("Player").GetComponent<PlayerHealth>();
        _gun = GameObject.FindWithTag("Weapon").GetComponent<Gun>();
    }

    private void Start()
    {
        //액션들 추가
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                StartCoroutine(nameof(ShowDamageBackground));
            }
            if (Input.GetKeyUp(KeyCode.O))
            {
                Debug.Log("Show Over Window");
                Canvas overPanel = Resources.Load<Canvas>("UI\\StageOverPanel");
                Instantiate(overPanel);
                Cursor.lockState = CursorLockMode.None;
            }
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                ShowOptionPanel();
                Cursor.lockState = CursorLockMode.None;
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                StartCoroutine(nameof(ShowUseBullet));
            }
            ChangeConditions();
        }
    }

    private void ChangeConditions()
    {
        ChangeHP();
        ChangeStamina();
        ChangeBullet();
    }

    private void ChangeBullet()
    {
        _bulletText.text = _gun.CurrentBulletCount.ToString() + " / " + _gun.CarryBulletCount.ToString();
    }

    private void ChangeHP()
    {
        //_hpBar.fillAmount = _health.   체력을 가져올 수 있게 되면 사용.
    }

    private void ChangeStamina()
    {
        _staminaBar.fillAmount = _player.Stamina.CurrentStamina / 100f;
        _staminaText.text = _player.Stamina.CurrentStamina.ToString("F0");
    }

    public void ShowOptionPanel()
    {
        Debug.Log("Show Option Window");
        Canvas optionPanel = Resources.Load<Canvas>("UI\\OptionPanel");
        Instantiate(optionPanel);
    }

    private IEnumerator ShowDamageBackground()
    {
        Color origin = _damagedbackground.color;
        float duration = 0.2f;
        float elapsed = 0f;
        while (duration > elapsed)
        {
            _damagedbackground.color = Color.Lerp(Color.red, origin, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        _damagedbackground.color = origin;
    }

    private IEnumerator ShowUseBullet()
    {
        TMP_Text t = Resources.Load<TMP_Text>("UI\\BulletEffectText");
        TMP_Text _bulletEffect = Instantiate(t);
        _bulletEffect.transform.SetParent(transform);
        _bulletEffect.transform.position = new Vector3(_bulletText.transform.position.x - 30, _bulletText.transform.position.y , _bulletText.transform.position.z);
        _bulletEffect.text = _gun.CurrentBulletCount.ToString();

        float alpha = 1;
        while(alpha > 0)
        {
            Color color = new Color(1,1,1,alpha);
            _bulletEffect.color = color;
            _bulletEffect.gameObject.transform.position = new Vector3(_bulletEffect.transform.position.x, _bulletEffect.transform.position.y + 0.1f, _bulletEffect.transform.position.z);
            alpha -= Time.deltaTime;
            yield return null;
        }
        Destroy(_bulletEffect);
    }
}
