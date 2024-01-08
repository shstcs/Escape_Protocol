using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_MenuPanel : MonoBehaviour
{
    private Animator CameraObject;

    // campaign button sub menu
    [Header("MENUS")]
    public GameObject mainMenu;
    public GameObject firstMenu;
    public GameObject playMenu;
    public GameObject exitMenu;

    [Header("PANELS")]
    public GameObject mainCanvas;
    public GameObject PanelControls;
    public GameObject PanelVideo;
    public GameObject PanelGame;
    public GameObject PanelMovement;
    public GameObject PanelCombat;
    public GameObject PanelGeneral;

    [Header("SETTINGS SCREEN")]
    public GameObject lineGame;
    public GameObject lineVideo;
    public GameObject lineControls;
    public GameObject lineMovement;
    public GameObject lineCombat;
    public GameObject lineGeneral;

    [Header("LOADING SCREEN")]
    public GameObject loadingMenu;
    public Slider loadingBar;
    public TMP_Text loadPromptText;

    [Header("SFX")]
    public AudioSource hoverSound;
    public AudioSource sliderSound;
    public AudioSource swooshSound;

    void Start()
    {
        CameraObject = transform.GetComponent<Animator>();

        playMenu.SetActive(false);
        exitMenu.SetActive(false);
        firstMenu.SetActive(true);
        mainMenu.SetActive(true);
        Main.Game.IsClear = false;
    }

    public void PlayCampaign()
    {
        exitMenu.SetActive(false);
        playMenu.SetActive(true);
    }

    public void ReturnMenu()
    {
        playMenu.SetActive(false);
        exitMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void LoadStage1(string scene)
    {
        if (scene != "")
        {
            StartCoroutine(LoadAsynchronously(scene));
        }
    }

    public void LoadStage2(string scene)
    {
        StartCoroutine(LoadAsynchronously(scene));

        /*
        if (scene != "" && Main.Game.IsStage1Clear)
        {
            StartCoroutine(LoadAsynchronously(scene));
        }
        else Debug.Log("1단계를 클리어하지 않았습니다.");
        */
    }

    public void DisablePlayCampaign()
    {
        playMenu.SetActive(false);
    }

    public void Position2()
    {
        DisablePlayCampaign();
        CameraObject.SetFloat("Animate", 1);
    }

    public void Position1()
    {
        CameraObject.SetFloat("Animate", 0);
    }

    void DisablePanels()
    {
        PanelControls.SetActive(false);
        PanelVideo.SetActive(false);
        PanelGame.SetActive(false);

        lineGame.SetActive(false);
        lineControls.SetActive(false);
        lineVideo.SetActive(false);

        PanelMovement.SetActive(false);
        lineMovement.SetActive(false);
        PanelCombat.SetActive(false);
        lineCombat.SetActive(false);
        PanelGeneral.SetActive(false);
        lineGeneral.SetActive(false);
    }

    public void GamePanel()
    {
        DisablePanels();
        PanelGame.SetActive(true);
        lineGame.SetActive(true);
    }

    public void VideoPanel()
    {
        DisablePanels();
        PanelVideo.SetActive(true);
        lineVideo.SetActive(true);
    }

    public void ControlsPanel()
    {
        DisablePanels();
        PanelControls.SetActive(true);
        lineControls.SetActive(true);
    }

    public void MovementPanel()
    {
        DisablePanels();
        PanelMovement.SetActive(true);
        lineMovement.SetActive(true);
    }

    public void CombatPanel()
    {
        DisablePanels();
        PanelCombat.SetActive(true);
        lineCombat.SetActive(true);
    }

    public void GeneralPanel()
    {
        DisablePanels();
        PanelGeneral.SetActive(true);
        lineGeneral.SetActive(true);
    }

    public void PlayHover()
    {
        hoverSound.Play();
    }

    public void PlaySFXHover()
    {
        sliderSound.Play();
    }

    public void PlaySwoosh()
    {
        swooshSound.Play();
    }

    // Are You Sure - Quit Panel Pop Up
    public void AreYouSure()
    {
        exitMenu.SetActive(true);
        DisablePlayCampaign();
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    IEnumerator LoadAsynchronously(string sceneName)
    { // scene name is just the name of the current scene being loaded
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        mainCanvas.SetActive(false);
        loadingMenu.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .95f);
            loadingBar.value = progress;

            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
