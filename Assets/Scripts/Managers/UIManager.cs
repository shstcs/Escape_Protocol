using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private void Awake()
    {
        
    }
    private void Start()
    {
        Main.Game.OnStageStart += SetHUD;
    }

    private void Update()
    {
        
    }
    public void SetHUD()
    {
        Canvas _hud = Resources.Load<Canvas>("HUDPanel");
        Instantiate(_hud);
    }
}
