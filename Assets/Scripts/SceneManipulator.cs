using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManipulator : Singleton<SceneManipulator>
{
    [SerializeField] private const string MainMenu = "MainMenu";
    [SerializeField] private const string Level_1 = "Level_1";


    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MainMenu);
    }

    public void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene++);
    }

    public void RestartLevel()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

}
