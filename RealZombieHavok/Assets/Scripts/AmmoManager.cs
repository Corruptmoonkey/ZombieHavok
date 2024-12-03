/*
 * made by Steven Pichleman
 * 11/23/2024
 * AMmoManager
 * manages the ammo count for the various guns. keeps track per gun name, not object.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    private WeaponManager WeaponManager;
    public static AmmoManager Instance { get; set; }
    public Dictionary<string, int> AmmoReserves = new Dictionary<string, int>();
    // all ammo is given based on a multiple of the magazine size
    public const int StartingMags = 2;
    public const int BonusMags = 3;
    public const int MaxMags = 4;
    public TextMeshProUGUI ammoDisplay;
    public TextMeshProUGUI ReserveAmmoDisplay;

    private void Awake()
    {
        if (Instance != null && Instance != this)

        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        WeaponManager = WeaponManager.Instance;
    }
    // add more ammo. Prioritizes held weapon first.
    public void AddAmmo(Collider AmmoBox)
    {
                //do not try to give ammo if there is no held weapon
                if (WeaponManager.activeWeaponSlot.transform.childCount == 0) return;
                var ActiveWeapon = WeaponManager.activeWeaponSlot.transform.GetChild(0).GetComponent<WeaponScript>();

                //check if active weapon is registered in dictionary. If not, set ammo as StartingMags. 
                if (AmmoReserves.ContainsKey(ActiveWeapon.Name))
                {
                    //do not give ammo if full
                     if (AmmoReserves[ActiveWeapon.Name] >= ActiveWeapon.magazineSize * MaxMags) return;

                   // if ammo exists, add BonusMags
                    //clamp max mag size to MaxMags
                    AmmoReserves[ActiveWeapon.Name] = 
                        Math.Min(AmmoReserves[ActiveWeapon.Name] + ActiveWeapon.magazineSize * BonusMags, 
                        ActiveWeapon.magazineSize * MaxMags);
                }
                else
                { // if weapon is not in dictionary, add StartingMags
                   // InitializeAmmo(ActiveWeapon);
                }

                Destroy(AmmoBox.gameObject);
                 UpdateReserveText(ActiveWeapon);
        
    }
    // handles when ammo is removed from reserve during reload
    public int RemoveFromReserve(WeaponScript Weapon, int BulletsLeft)
    {
        int NewMag;
        if (AmmoReserves[Weapon.Name] < Weapon.magazineSize - BulletsLeft)
        {
            NewMag = AmmoReserves[Weapon.Name] + BulletsLeft;
            AmmoReserves[Weapon.Name] = 0;
        }
        else
        {
            NewMag = Weapon.magazineSize;
            AmmoReserves[Weapon.Name] -= Weapon.magazineSize - BulletsLeft;
        }
        UpdateReserveText(Weapon);
        return NewMag;
    }
    //give StartingMags to newly held weapon.
    public void InitializeAmmo(string aWeapon, int aMagazineSize)
    {
       AmmoReserves.TryAdd(aWeapon, aMagazineSize* StartingMags);
        Debug.Log($"weapon: {aWeapon}, {aMagazineSize * StartingMags}");

    }
    public void UpdateReserveText(WeaponScript Weapon)
    {
        ReserveAmmoDisplay.text = "/" + AmmoReserves[Weapon.Name].ToString();

    }
    // todo: only need to give initial ammo when gun is first picked up, run unique script for that.
    //also update text for ammo in stockpile. maybe do ammo in mag as well: currently updated using weaponscript.
}
