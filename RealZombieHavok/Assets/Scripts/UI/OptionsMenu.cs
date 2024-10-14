using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] float MouseSensitivity;
    [SerializeField] GameObject sldrMouseSensitivity;
    [SerializeField] GameObject MainCamera;
    [SerializeField] SettingsManager SettingsManager;
     PlayerCam PlayerCam;

        public void Start()
    {
        SettingsManager = GameObject.FindGameObjectWithTag("SettingsManager").GetComponent<SettingsManager>(); // necessary to find this automatically as it will be referenced across scenes.
        MouseSensitivity = SettingsManager.MouseSensitivity; // load existing value
        PlayerCam = MainCamera.GetComponent<PlayerCam>();
        sldrMouseSensitivity.GetComponent<Slider>().value = MouseSensitivity;
    }
    public void ChangeMouseSensitivity()
    {
        MouseSensitivity = sldrMouseSensitivity.GetComponent<Slider>().value;
        PlayerCam.sensitivityX = MouseSensitivity;
        PlayerCam.sensitivityY = MouseSensitivity;
        SettingsManager.MouseSensitivity = MouseSensitivity; // save value

    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
