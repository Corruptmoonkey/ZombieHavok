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
    [SerializeField] Image _OptionsMenu;
    [SerializeField] GameObject Player;
    //MoveCamera MoveCamera;
    public void Start()
    {
    //    MoveCamera = Player.GetComponent<MoveCamera>();
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
        // toggles between main pause and options menu
        _PauseMenu.gameObject.SetActive(_OptionsMenu.gameObject.activeSelf);
        _OptionsMenu.gameObject.SetActive(!_PauseMenu.gameObject.activeSelf);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void TogglePauseMenu()
    {
        Time.timeScale = IsPaused ? 1.0f : 0.0f;
        // unlocks cursor to allow it to select.
        // disable mouse aiming
       // MoveCamera.enabled = IsPaused;
        _PauseMenu.gameObject.SetActive(!IsPaused);
        _OptionsMenu.gameObject.SetActive(false);
        Cursor.lockState = IsPaused ? CursorLockMode.Locked : CursorLockMode.Confined;
        IsPaused = !IsPaused;
    }
}
