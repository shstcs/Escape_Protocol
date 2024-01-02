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

    [Header("Quest")]
    [SerializeField] private TMP_Text _questText;

    private void Start()
    {
        //액션들 추가
    }

    private void Update()
    {
        if(Time.timeScale > 0)
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                StartCoroutine(nameof(ShowDamageBackground));
            }
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                ShowOptionPanel();
            }
            ChangeConditions();
        } 
    }

    private void ChangeConditions()
    {
        ChangeHP();
        ChangeStamina();
    }

    private void ChangeHP()
    {

    }

    private void ChangeStamina()
    {

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
        while(duration > elapsed)
        {
            _damagedbackground.color = Color.Lerp(Color.red, origin, elapsed/duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        _damagedbackground.color = origin;
    }
}
