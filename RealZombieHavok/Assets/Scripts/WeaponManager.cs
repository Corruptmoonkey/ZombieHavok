using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weapons;  // Array to store all weapon GameObjects
    private int currentWeaponIndex = 0;  // Track the currently active weapon

    void Start()
    {
        ActivateWeapon(currentWeaponIndex);  // Activate the first weapon by default
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)  // Scroll up
        {
            SwitchWeapon(1);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)  // Scroll down
        {
            SwitchWeapon(-1);
        }

        // Optional: Use number keys to switch weapons
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ActivateWeapon(0);  // Switch to weapon 1
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ActivateWeapon(1);  // Switch to weapon 2
    }

    void SwitchWeapon(int direction)
    {
        // Deactivate the current weapon
        weapons[currentWeaponIndex].SetActive(false);

        // Calculate the next weapon index
        currentWeaponIndex = (currentWeaponIndex + direction + weapons.Length) % weapons.Length;

        // Activate the new weapon
        ActivateWeapon(currentWeaponIndex);
    }

    void ActivateWeapon(int index)
    {
        // Deactivate all weapons
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }

        // Activate the selected weapon
        weapons[index].SetActive(true);
    }
}
