using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool IsPaused = false;
    [SerializeField] Image _PauseMenu;
    [SerializeField] GameObject Player;
    MouseMove MouseMove;
    public void Start()
    {
        MouseMove = Player.GetComponent<MouseMove>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // unlocks cursor to allow it to select.
            Cursor.lockState = IsPaused ? CursorLockMode.Locked : CursorLockMode.Confined;
            //disable mouse aiming
            MouseMove.enabled = !MouseMove.enabled;
            _PauseMenu.gameObject.SetActive(!IsPaused);
            IsPaused = !IsPaused;
        }
    }
    public void Resume()
    {
        _PauseMenu.gameObject.SetActive(!IsPaused);
        IsPaused = !IsPaused;
    }
    public void Options()
    {
        SceneManager.LoadScene("OptionsMenu");
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
