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

    //PlayerCam PlayerCam;

        public void Start()
    {
        MouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 100f);
        Difficulty = PlayerPrefs.GetFloat("Difficulty", 0.5f);
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
        //currently there is no difficulty implemented.
        Difficulty = sldrDifficulty.value;
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
