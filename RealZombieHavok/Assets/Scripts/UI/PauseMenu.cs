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
    OptionsMenu OptionsMenu;
    public void Start()
    {
        OptionsMenu = GetComponent<OptionsMenu>();
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
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
        OptionsMenu.SaveChanges();
    }

    public void TogglePauseMenu()
    {
        Time.timeScale = IsPaused ? 1.0f : 0.0f;
        // unlocks cursor to allow it to select.
        _PauseMenu.gameObject.SetActive(!IsPaused);
        _OptionsMenu.gameObject.SetActive(false);
        Cursor.lockState = IsPaused ? CursorLockMode.Locked : CursorLockMode.Confined;
        Cursor.visible = !IsPaused;
        //disable weapon to prevent it from shooting with mouse clicks.
        Player.GetComponentInChildren<WeaponScript>().isActiveWeapon = IsPaused;

        //save changes to options when unpausing.
        OptionsMenu.SaveChanges();

        IsPaused = !IsPaused;
    }
}
