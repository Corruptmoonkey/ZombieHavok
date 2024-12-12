using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Canvas MainCanvas;
    public Canvas HelpCanvas;
    private bool IsHelped;
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
    public void Help()
    {
        MainCanvas.gameObject.SetActive(!IsHelped);
        HelpCanvas.gameObject.SetActive(IsHelped);
        IsHelped = !IsHelped;
    }
}
