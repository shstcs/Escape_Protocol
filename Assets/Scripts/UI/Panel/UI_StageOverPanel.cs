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
        StartCoroutine(nameof(TimeStopDelay));
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
    private IEnumerator TimeStopDelay()
    {
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 0;
    }
}
