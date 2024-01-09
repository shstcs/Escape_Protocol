using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private void Start()
    {
        Main.Game.OnStageStart += SetHUD;
    }
    public void SetHUD()
    {
        Canvas _hud = Resources.Load<Canvas>("HUDPanel");
        Instantiate(_hud);
    }
    public IEnumerator TimeStopDelay()
    {
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 0;
    }
}
