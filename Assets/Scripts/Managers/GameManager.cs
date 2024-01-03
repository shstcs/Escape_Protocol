using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityAction OnStageStart;
    public UnityAction OnStageOver;
    public UnityAction OnKeyGet;
    public bool IsClear { get; set; }
    public bool IsStage1Clear { get; set; }

    public void CallStageStart()
    {
        OnStageStart?.Invoke();
    }

    public void CallStageOver()
    {
        OnStageOver?.Invoke();
    }

    public void CallkeyGet()
    {
        OnKeyGet?.Invoke();
    }

    private void Start()
    {

    }

    private void Update()
    {

    }
}
