/*
OptionsMenu 
by Steven Pichelman
10/20/2024
this script handles any changes to the options, whether it is ingame in the pause menu or in the options menu from the main menu.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] float MouseSensitivity;
    [SerializeField] float Difficulty;
    [SerializeField] Slider sldrMouseSensitivity;
    [SerializeField] Slider  sldrDifficulty;
    [SerializeField] PlayerCam PlayerCam;
    [SerializeField] Player Player;

    public void Start()
    {
        MouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 100f);
        Difficulty = PlayerPrefs.GetFloat("Difficulty", 1f);
        PlayerCam.sensitivityX = MouseSensitivity;
        PlayerCam.sensitivityY = MouseSensitivity;

        sldrMouseSensitivity.value = MouseSensitivity;
        sldrMouseSensitivity.onValueChanged.AddListener(delegate { ChangeMouseSensitivity(); });

        sldrDifficulty.value = Difficulty;
        sldrDifficulty.onValueChanged.AddListener(delegate { ChangeDifficulty(); });
    }
    public void ChangeMouseSensitivity()
    {
        MouseSensitivity = sldrMouseSensitivity.value;
        PlayerCam.sensitivityX = MouseSensitivity;
        PlayerCam.sensitivityY = MouseSensitivity;
    }
    public void ChangeDifficulty()
    {
        Difficulty = sldrDifficulty.value;
        Player.HP = (int)Mathf.RoundToInt(100 / Difficulty); //removes or adds health
        Player.playerHealthUI.text = $"Health: {Player.HP}"; //update

    }
    public void SaveChanges()
    {
        //save values changed
        PlayerPrefs.SetFloat("MouseSensitivity", MouseSensitivity);
        PlayerPrefs.SetFloat("Difficulty", Difficulty);
    }
    public void ToMainMenu()
    {
        SaveChanges();
        SceneManager.LoadScene("MainMenu");
    }

}
