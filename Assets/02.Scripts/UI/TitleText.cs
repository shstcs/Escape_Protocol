using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleText : MonoBehaviour
{
    private TMP_Text _title;
    private float _duration = 5f;
    private void Start()
    {
        _title = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        _duration -= Time.deltaTime;
        if (_duration < .95f && _duration > .85f)
        {
            _title.alpha = 0.5f;
        }
        else if (_duration < .75f && _duration > .65f)
        {
            _title.alpha = 1f;
        }
        else if (_duration < .55f && _duration > .45f)
        {
            _title.alpha = 0.3f;
        }
        else if (_duration < .45f && _duration > .35f)
        {
            _title.alpha = 0;
        }
        else if(_duration < 0)
        {
            _title.alpha = 1f;
            _duration = 5;
        }
    }
}
