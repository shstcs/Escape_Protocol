using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_StageOverPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _overText;

    private void Start()
    {
        Time.timeScale = 0.0f;
        ChangeText();
    }

    public void ChangeText()
    {
        _overText.text = Main.Game.IsClear ? "Stage Clear" : "Stage Failed";
    }

    public void ReturnToMenu(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}
