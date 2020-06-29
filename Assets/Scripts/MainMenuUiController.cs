using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUiController : MonoBehaviour
{
    [SerializeField] private const string Level_1 = "Level_1";

    public void LoadLevel1()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(Level_1);
    }
}
