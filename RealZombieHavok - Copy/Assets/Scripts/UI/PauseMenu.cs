using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class PauseMenu : MonoBehaviour
{
    bool IsPaused = false;
    [SerializeField] Image _PauseMenu;
    [SerializeField] GameObject Player;
    //MouseMove MouseMove;
    public void Start()
    {
        //MouseMove = Player.GetComponent<MouseMove>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           TogglePauseMenu();
        }
    }
    public void Resume()
    {
        TogglePauseMenu();
    }
    public void Options()
    {
        SceneManager.LoadScene("OptionsMenu");
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
    public void TogglePauseMenu()
    {
        Time.timeScale = IsPaused ? 1.0f : 0.0f;
        Debug.Log(Time.timeScale.ToString());
        // unlocks cursor to allow it to select.
        Cursor.lockState = IsPaused ? CursorLockMode.Locked : CursorLockMode.Confined;
        //disable mouse aiming
       // MouseMove.enabled = IsPaused;
        _PauseMenu.gameObject.SetActive(!IsPaused);
        IsPaused = !IsPaused;
    }
}
