using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_OptionPanel : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(nameof(TimeStopDelay));

    }

    public void ReturnToMenu(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    public void CancelOptionPanel()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Destroy(gameObject);
    }

    public void ControlVolumeSlider(Slider musicSlider)
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void ControlVolume()
    {
        GameObject _sound = GameObject.Find("BackgroundMusic");
        _sound.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume");
    }


    private IEnumerator TimeStopDelay()
    {
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 0;
    }
}
