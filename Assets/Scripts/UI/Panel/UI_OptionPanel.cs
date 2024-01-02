using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_OptionPanel : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0.0f;
    }

    public void ReturnToMenu(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    public void CancelOptionPanel()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
