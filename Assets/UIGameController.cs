using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameController : MonoBehaviour
{
    public TextMeshProUGUI transformationCount;

    public Image[] images;

    public float transparentCol = 0.3f;

    public GameObject restartPanel;

    public GameObject finishGamePanel;

    private void Start()
    {
        Time.timeScale = 1;

        images = GetComponentsInChildren<Image>();
        foreach(Image img in images)
        {
            Color col = img.color;
            col.a = transparentCol;
            img.color = col;
        }
    }

    public void KeyPicked(KeyData data)
    {
        foreach (Image img in images)
        {
            if(img.gameObject.name == data.KEY_TYPE.ToString())
            {
                Color col = img.color;
                col.a = 1f;
                img.color = col;
            }
        }
    }

    public void ShowRestartButton()
    {
        restartPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void LoadMainMenu()
    {
        SceneManipulator.Instance.LoadMainMenu();
    }

    public void Restart()
    {
        SceneManipulator.Instance.RestartLevel();
    }

    public void FinishGame()
    {
        finishGamePanel.SetActive(true);
        Time.timeScale = 0;
    }

}
