using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Fields

    public UnityAction OnStageStart;
    public UnityAction OnStageOver;
    public UnityAction OnKeyGet;
    public UnityAction OnDoorOpen;
    public UnityAction OnWeaponGet;

    #endregion

    #region Properties

    public bool IsClear { get; set; }
    public bool IsStage1Clear { get; set; }

    #endregion

    #region Methods

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

    public void CallDoorOpen()
    {
        OnDoorOpen?.Invoke();
    }

    public void CallWeaponGet()
    {
        OnWeaponGet?.Invoke();
    }

    #endregion
}
