using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_HUDPanel : MonoBehaviour
{
    #region Factors
    [Header("Condition")]
    [SerializeField] private Image _hpBar;
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private Image _staminaBar;
    [SerializeField] private TMP_Text _staminaText;
    [SerializeField] private TMP_Text _bulletText;
    [SerializeField] private Image _damagedbackground;
    [SerializeField] private Transform _questTransform; 

    private UI_Quest _quest;

    private Player _player;
    private PlayerHealth _health;
    private Gun _gun;

    private Color _staminaColor;
    private Color _hpColor;

    #endregion

    #region LifeCycle
    private void Start()
    {
        _player = Main.Player;
        _health = _player.gameObject.GetComponent<PlayerHealth>();

        CreateKeyQuest();
        ChangeWeapon();
        ConnetActions();
        _staminaColor = _staminaBar.color;
        _hpColor = _hpBar.color;
    }
    private void Update()
    {
        if (Time.timeScale > 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ShowOptionPanel();
                Cursor.lockState = CursorLockMode.None;
            }

            ChangeConditions();
        }
    }
    private void OnDestroy()
    {
        UnConnectActions();
    }
    #endregion

    #region Connect to Actions
    private void ConnetActions()
    {
        Main.Game.OnKeyGet += CreateDoorQuest;
        Main.Game.OnDoorOpen += CreateKeyQuest;
        Main.Game.OnWeaponGet += ChangeWeapon;
        Main.Game.OnStageOver += ShowOverPanel;
        _health.OnDie += ShowOverPanel;
    }

    private void UnConnectActions()
    {
        Main.Game.OnKeyGet -= CreateDoorQuest;
        Main.Game.OnDoorOpen -= CreateKeyQuest;
        Main.Game.OnWeaponGet -= ChangeWeapon;
        Main.Game.OnStageOver -= ShowOverPanel;
        _health.OnDie -= ShowOverPanel;
    } 
    #endregion

    #region Change Conditions
    private void ChangeConditions()
    {
        ChangeHP();
        ChangeStamina();
        ChangeBullet();
    }
    private void ChangeWeapon()
    {
        _gun = Main.Player.GunController.CurrentGun;
    }

    private void ChangeBullet()
    {
        string old = _bulletText.text;

        _bulletText.text = _gun.CurrentBulletCount.ToString() + " / " + _gun.CarryBulletCount.ToString();
        if (old != _bulletText.text) StartCoroutine(nameof(ShowUseBullet));
    }

    private void ChangeHP()
    {
        float old = _hpBar.fillAmount;
        _hpBar.fillAmount = _health.Health / 100;
        _hpText.text = _health.Health.ToString("F0");
    }

    private void ChangeStamina()
    {
        float old = _staminaBar.fillAmount;
        _staminaBar.fillAmount = _player.Stamina.CurrentStamina / 100f;
        _staminaText.text = _player.Stamina.CurrentStamina.ToString("F0");
        _staminaBar.color = old > _staminaBar.fillAmount ? new Color(.7f, 1, .7f) : _staminaColor;
    }
    #endregion

    #region Show Panel
    public void StartDamageBackground()
    {
        StartCoroutine(nameof(ShowDamageBackground));
        StartCoroutine(nameof(HpDamaged));
    }

    public void ShowOverPanel()
    {
        Canvas overPanel = Resources.Load<Canvas>("UI\\Panel\\StageOverPanel");
        Instantiate(overPanel);
        Cursor.lockState = CursorLockMode.None;
    }

    public void ShowOptionPanel()
    {
        Debug.Log("Show Option Window");
        Canvas optionPanel = Resources.Load<Canvas>("UI\\Panel\\OptionPanel");
        Instantiate(optionPanel);
    }

    #endregion

    #region Quest
    private void CreateDoorQuest()
    {
        StartCoroutine(CreateQuests("문을 열어 탈출하라"));
    }

    private void CreateKeyQuest()
    {
        if (Main.Player.KeyCheck.Red)
        {
            StartCoroutine(CreateQuests("탈출하라"));
        }
        else StartCoroutine(CreateQuests("열쇠를 찾아라"));
    }
    #endregion

    #region Coroutine
    private IEnumerator CreateQuests(string questName)
    {
        if (_quest != null)
        {
            float _duration = 1f;
            while(_duration > 0)
            {
                _quest.GetComponent<Image>().color = new Color(1, 1, 1, _duration);
                _quest.GetComponentInChildren<TMP_Text>().color = new Color(0, 0, 0, _duration);
                _duration -= Time.deltaTime;
                yield return null;
            }
            Destroy(_quest.gameObject);
            _quest = null;
        }

        Image img = Resources.Load<Image>("UI\\QuestBox");
        _quest = Instantiate(img).GetComponent<UI_Quest>();
        Transform questPos = gameObject.transform.Find("QuestLayout");
        _quest.transform.SetParent(questPos);
        _quest.transform.position = questPos.position;
        _quest.SetText(questName);

        float duration = 0f;
        while (duration < 1)
        {
            _quest.GetComponent<Image>().color = new Color(1, 1, 1, duration);
            _quest.GetComponentInChildren<TMP_Text>().color = new Color(0, 0, 0, duration);
            duration += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator ShowDamageBackground()
    {
        Color origin = _damagedbackground.color;
        float duration = 0.2f;
        float elapsed = 0f;
        while (duration > elapsed)
        {
            _damagedbackground.color = Color.Lerp(new Color32(255, 0, 0, 60), origin, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        _damagedbackground.color = origin;
    }
    private IEnumerator HpDamaged()
    {
        _hpBar.color = new Color(1, .7f, .7f);
        yield return new WaitForSeconds(1);
        _hpBar.color = _hpColor;
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
    #endregion
}
