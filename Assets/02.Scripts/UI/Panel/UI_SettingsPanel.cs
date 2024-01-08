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
    public GameObject guneffectstext;

    [Header("GAME SETTINGS")]
    public GameObject difficultynormaltext;
    public GameObject difficultynormaltextLINE;
    public GameObject difficultyhardcoretext;
    public GameObject difficultyhardcoretextLINE;

    [Header("CONTROLS SETTINGS")]
    // sliders
    public GameObject musicSlider;
    public GameObject sensitivityXSlider;
    public GameObject sensitivityYSlider;
    public GameObject mouseSmoothSlider;

    //private float sliderValue = 0.0f;
    private float sliderValueXSensitivity = 0.0f;
    private float sliderValueYSensitivity = 0.0f;
    private float sliderValueSmoothing = 0.0f;

    [Header("Key Binding")]
    public GameObject keyConfirm;
    private bool waitForKey;
    private TMP_Text curKeyText;
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

        //check gun effect
        guneffectstext.GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("GunEffects") == 0 ? "off" : "on";
    }

    public void Update()
    {
        sliderValueXSensitivity = sensitivityXSlider.GetComponent<Slider>().value;
        sliderValueYSensitivity = sensitivityYSlider.GetComponent<Slider>().value;
        sliderValueSmoothing = mouseSmoothSlider.GetComponent<Slider>().value;
        if (waitForKey)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        Debug.Log("Pressed key: " + keyCode);
                        keyConfirm.SetActive(false);
                        curKeyText.text = keyCode.ToString();
                        waitForKey = false;
                        break;
                    }
                }
            }
        }
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
    }

    public void HardcoreDifficulty()
    {
        difficultyhardcoretextLINE.gameObject.SetActive(true);
        difficultynormaltextLINE.gameObject.SetActive(false);
        PlayerPrefs.SetInt("NormalDifficulty", 0);
    }

    public void GunEffects()
    {
        if (PlayerPrefs.GetInt("GunEffects") == 0)
        {
            PlayerPrefs.SetInt("GunEffects", 1);
            guneffectstext.GetComponent<TMP_Text>().text = "on";
        }
        else if (PlayerPrefs.GetInt("GunEffects") == 1)
        {
            PlayerPrefs.SetInt("GunEffects", 0);
            guneffectstext.GetComponent<TMP_Text>().text = "off";
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

    public void BindingKey(TMP_Text keyText)
    {
        curKeyText = keyText;
        waitForKey = true;
        keyConfirm.SetActive(true);
    }
}

