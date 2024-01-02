using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;


public class UI_SettingsPanel : MonoBehaviour
{
    [Header("VIDEO SETTINGS")]
    public GameObject fullscreentext;
    public GameObject texturelowtextLINE;
    public GameObject texturemedtextLINE;
    public GameObject texturehightextLINE;
    public GameObject cameraeffectstext;

    [Header("GAME SETTINGS")]
    public GameObject difficultynormaltext;
    public GameObject difficultynormaltextLINE;
    public GameObject difficultyhardcoretext;
    public GameObject difficultyhardcoretextLINE;

    [Header("CONTROLS SETTINGS")]
    public GameObject invertmousetext;

    // sliders
    public GameObject musicSlider;
    public GameObject sensitivityXSlider;
    public GameObject sensitivityYSlider;
    public GameObject mouseSmoothSlider;

    //private float sliderValue = 0.0f;
    private float sliderValueXSensitivity = 0.0f;
    private float sliderValueYSensitivity = 0.0f;
    private float sliderValueSmoothing = 0.0f;


    public void Start()
    {
        // check difficulty
        if (PlayerPrefs.GetInt("NormalDifficulty") == 1)
        {
            difficultynormaltextLINE.gameObject.SetActive(true);
            difficultyhardcoretextLINE.gameObject.SetActive(false);
        }
        else
        {
            difficultyhardcoretextLINE.gameObject.SetActive(true);
            difficultynormaltextLINE.gameObject.SetActive(false);
        }

        // check slider values
        musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");
        sensitivityXSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("XSensitivity");
        sensitivityYSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("YSensitivity");
        mouseSmoothSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MouseSmoothing");

        // check full screen
        if (Screen.fullScreen == true)
        {
            fullscreentext.GetComponent<TMP_Text>().text = "on";
        }
        else if (Screen.fullScreen == false)
        {
            fullscreentext.GetComponent<TMP_Text>().text = "off";
        }

        // check mouse inverse
        if (PlayerPrefs.GetInt("Inverted") == 0)
        {
            invertmousetext.GetComponent<TMP_Text>().text = "off";
        }
        else if (PlayerPrefs.GetInt("Inverted") == 1)
        {
            invertmousetext.GetComponent<TMP_Text>().text = "on";
        }

        // check texture quality
        if (PlayerPrefs.GetInt("Textures") == 0)
        {
            QualitySettings.globalTextureMipmapLimit = 2;
            texturelowtextLINE.gameObject.SetActive(true);
            texturemedtextLINE.gameObject.SetActive(false);
            texturehightextLINE.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Textures") == 1)
        {
            QualitySettings.globalTextureMipmapLimit = 1;
            texturelowtextLINE.gameObject.SetActive(false);
            texturemedtextLINE.gameObject.SetActive(true);
            texturehightextLINE.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Textures") == 2)
        {
            QualitySettings.globalTextureMipmapLimit = 0;
            texturelowtextLINE.gameObject.SetActive(false);
            texturemedtextLINE.gameObject.SetActive(false);
            texturehightextLINE.gameObject.SetActive(true);
        }
    }

    public void Update()
    {
        //sliderValue = musicSlider.GetComponent<Slider>().value;
        sliderValueXSensitivity = sensitivityXSlider.GetComponent<Slider>().value;
        sliderValueYSensitivity = sensitivityYSlider.GetComponent<Slider>().value;
        sliderValueSmoothing = mouseSmoothSlider.GetComponent<Slider>().value;
    }

    public void FullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;

        if (Screen.fullScreen == true)
        {
            fullscreentext.GetComponent<TMP_Text>().text = "on";
        }
        else if (Screen.fullScreen == false)
        {
            fullscreentext.GetComponent<TMP_Text>().text = "off";
        }
    }

    public void MusicSlider()
    {
        //PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.GetComponent<Slider>().value);
    }

    public void SensitivityXSlider()
    {
        PlayerPrefs.SetFloat("XSensitivity", sliderValueXSensitivity);
    }

    public void SensitivityYSlider()
    {
        PlayerPrefs.SetFloat("YSensitivity", sliderValueYSensitivity);
    }

    public void SensitivitySmoothing()
    {
        PlayerPrefs.SetFloat("MouseSmoothing", sliderValueSmoothing);
        Debug.Log(PlayerPrefs.GetFloat("MouseSmoothing"));
    }

    public void NormalDifficulty()
    {
        difficultyhardcoretextLINE.gameObject.SetActive(false);
        difficultynormaltextLINE.gameObject.SetActive(true);
        PlayerPrefs.SetInt("NormalDifficulty", 1);
        PlayerPrefs.SetInt("HardCoreDifficulty", 0);
    }

    public void HardcoreDifficulty()
    {
        difficultyhardcoretextLINE.gameObject.SetActive(true);
        difficultynormaltextLINE.gameObject.SetActive(false);
        PlayerPrefs.SetInt("NormalDifficulty", 0);
        PlayerPrefs.SetInt("HardCoreDifficulty", 1);
    }

    public void InvertMouse()
    {
        if (PlayerPrefs.GetInt("Inverted") == 0)
        {
            PlayerPrefs.SetInt("Inverted", 1);
            invertmousetext.GetComponent<TMP_Text>().text = "on";
        }
        else if (PlayerPrefs.GetInt("Inverted") == 1)
        {
            PlayerPrefs.SetInt("Inverted", 0);
            invertmousetext.GetComponent<TMP_Text>().text = "off";
        }
    }

    public void CameraEffects()
    {
        if (PlayerPrefs.GetInt("CameraEffects") == 0)
        {
            PlayerPrefs.SetInt("CameraEffects", 1);
            cameraeffectstext.GetComponent<TMP_Text>().text = "on";
        }
        else if (PlayerPrefs.GetInt("CameraEffects") == 1)
        {
            PlayerPrefs.SetInt("CameraEffects", 0);
            cameraeffectstext.GetComponent<TMP_Text>().text = "off";
        }
    }

    public void TexturesLow()
    {
        PlayerPrefs.SetInt("Textures", 0);
        QualitySettings.globalTextureMipmapLimit = 2;
        texturelowtextLINE.gameObject.SetActive(true);
        texturemedtextLINE.gameObject.SetActive(false);
        texturehightextLINE.gameObject.SetActive(false);
    }

    public void TexturesMed()
    {
        PlayerPrefs.SetInt("Textures", 1);
        QualitySettings.globalTextureMipmapLimit = 1;
        texturelowtextLINE.gameObject.SetActive(false);
        texturemedtextLINE.gameObject.SetActive(true);
        texturehightextLINE.gameObject.SetActive(false);
    }

    public void TexturesHigh()
    {
        PlayerPrefs.SetInt("Textures", 2);
        QualitySettings.globalTextureMipmapLimit = 0;
        texturelowtextLINE.gameObject.SetActive(false);
        texturemedtextLINE.gameObject.SetActive(false);
        texturehightextLINE.gameObject.SetActive(true);
    }
}
