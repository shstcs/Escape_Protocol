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
        
    }

    public void SetText(string text)
    {
        _questText.text = text;
    }

}
