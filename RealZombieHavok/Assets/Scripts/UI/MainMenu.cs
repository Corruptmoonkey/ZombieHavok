using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void Options()
    {
        SceneManager.LoadScene("OptionsMenu");

    }
    public void QuitGame()
    {
       Application.Quit();
        UnityEngine.Debug.Log("Application has quit.");
    }
}
