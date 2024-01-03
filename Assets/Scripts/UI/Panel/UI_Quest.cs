using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Quest : MonoBehaviour
{
    private TMP_Text _questText;

    private void Awake()
    {
        _questText = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        _questText.text = "열쇠를 찾아라";
    }

    public void SecondQuest()
    {
        _questText.text = "열쇠로 문을 열고 탈출하라";
    }

}
