using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UnityAction OnStageStart;
    public UnityAction OnStageOver;
    public UnityAction OnKeyGet;
    public UnityAction OnDoorOpen;

    private PlayerHealth _health;
    private UI_HUDPanel _uiHUD;

    private GameObject _playerPrefab;
    private GameObject _monsterPrefab;

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

    public void CallDoorOpen()
    {
        OnDoorOpen?.Invoke();
    }
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        _health = GameObject.Find("Player").GetComponent<PlayerHealth>();
        _uiHUD = FindObjectOfType<UI_HUDPanel>();
    }
    private void Start()
    {
        _health.OnDie += GameOver;
    }

    private void GameOver()
    {
        _uiHUD.ShowOverPanel();
    }

    private void Update()
    {

    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "StageScene1" || scene.name == "StageScene2")
        {
            if (!GameObject.FindWithTag("Player"))
            {
                Instantiate(Resources.Load<GameObject>("Player/Player"), GameObject.Find("PlayerSpawnPoint").transform.position, Quaternion.identity);
            }
            if (!GameObject.FindWithTag("Enemy"))
            {
                Instantiate(Resources.Load<GameObject>("Monster/Monster"), GameObject.Find("MonsterSpawnPoint").transform.position, Quaternion.identity);
            }
        }
    }
}
