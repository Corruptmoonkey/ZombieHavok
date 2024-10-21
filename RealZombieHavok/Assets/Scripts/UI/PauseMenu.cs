/*
PauseMenu 
by Steven Pichelman
10/20/2024
this handles the main pause menu behavior as well as the function of pausing the game.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using System.Linq;
using Unity.VisualScripting.FullSerializer;

public class PauseMenu : MonoBehaviour
{
    bool IsPaused = false;
    [SerializeField] Image _PauseMenu;
    [SerializeField] Image _OptionsMenu;
    [SerializeField] GameObject Player;
    OptionsMenu OptionsMenu;
    List<Canvas> OtherCanvases = new List<Canvas>();
    public void Start()
    {
        OptionsMenu = GetComponent<OptionsMenu>();
        FindCanvases();

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

        //disables other ui elements.
        foreach (Canvas c in OtherCanvases)
        {
            c.gameObject.SetActive(IsPaused);
        }

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


    public List<Canvas> FindCanvases()
    {   //this is overkill but a good exercise in finding gameObjects.
        //this finds all canvases and sort out the one that the pause menu is using.
        var FoundCanvases = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
        foreach (Canvas c in FoundCanvases)
        {
            if (c.name != "PauseCanvas")
            {
                OtherCanvases.Add(c);
            }
        }
        return OtherCanvases;
    }
}
