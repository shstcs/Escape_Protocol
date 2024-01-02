using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    #region Singleton

    private static Main _instance;
    private static bool _initialized;

    public static Main Instance
    {
        get
        {
            if(!_initialized)
            {
                _initialized = true;

                GameObject obj = GameObject.Find("@Main");
                if (obj==null)
                {
                    obj = new() { name = "@Main" };
                    obj.AddComponent<Main>();
                    DontDestroyOnLoad(obj);
                    _instance = obj.GetComponent<Main>();
                }
            }
            return _instance;
        }
    }
    #endregion

    private UIManager _ui = new();
    private GameManager _game = new();
    private SoundManager _sound = new();

    public static UIManager UI => Instance?._ui;
    public static GameManager Game => Instance?._game;
    public static SoundManager Sound => Instance?._sound;
}
