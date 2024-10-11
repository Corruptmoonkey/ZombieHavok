using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    public AudioSource ShootingChannel;

    public AudioSource reloadingSound1911;
    public AudioSource emptyMagazineSound1911;

    
    public AudioSource reloadingSoundSAW;
    public AudioSource emptyMagazineSoundSAW;

    public AudioClip M1911Shot;
    public AudioClip SAWShot;

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

   // public void PlayShootingSound()
  //  {
      //  switch ()
      //  {
        //    case WeaponModel.M1911:
           //     ShootingChannel.PlayOneShot(M1911Shot); break;
          //  case WeaponModel.SAW:
    //            ShootingChannel.PlayOneShot(SAWShot); break; 
       // }
   // }
//

   // public void PlayReloadSound() 
  //  {
      //  switch ()
       // {
          //  case WeaponModel.M1911:
           //     reloadingSound1911.Play(); break;
         //   case WeaponModel.SAW:
       //         reloadingSoundSAW.Play(); break;
      //  }
  //  }


}
