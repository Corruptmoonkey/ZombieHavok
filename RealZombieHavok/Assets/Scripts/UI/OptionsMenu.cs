using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] float MouseSensitivity;
    [SerializeField] float Difficulty;
    [SerializeField] GameObject sldrMouseSensitivity;
    [SerializeField] GameObject sldrDifficulty;
    [SerializeField] GameObject MainCamera;
     PlayerCam PlayerCam;

        public void Start()
    {
        MouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 100f);
        Difficulty = PlayerPrefs.GetFloat("Difficulty", 0.5f);
        PlayerCam = MainCamera.GetComponent<PlayerCam>();

        sldrMouseSensitivity.GetComponent<Slider>().value = MouseSensitivity;
        sldrDifficulty.GetComponent<Slider>().value = Difficulty;
    }
    public void ChangeMouseSensitivity()
    {
        MouseSensitivity = sldrMouseSensitivity.GetComponent<Slider>().value;
        PlayerCam.sensitivityX = MouseSensitivity;
        PlayerCam.sensitivityY = MouseSensitivity;
        PlayerPrefs.SetFloat("MouseSensitivity", MouseSensitivity);
    }
    public void ChangeDifficulty()
    {
        Difficulty = sldrDifficulty.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("Difficulty", Difficulty);
        Debug.Log(PlayerPrefs.GetFloat("Difficulty", 0.6f).ToString());
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
