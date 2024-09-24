using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{

    public static InteractionManager Instance { get; set; }

    public Weapon hoveredWeapon = null; 

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHitByRaycast = hit.transform.gameObject;

            // Check if the hit object has a Weapon component
            Weapon weaponComponent = objectHitByRaycast.GetComponent<Weapon>();
            if (weaponComponent != null && !weaponComponent.isActiveWeapon)
            {
                // Disable the outline on the previously hovered weapon (if any)
                if (hoveredWeapon != null && hoveredWeapon != weaponComponent)
                {
                    var outlineComponent = hoveredWeapon.GetComponent<Outline>();
                    if (outlineComponent != null)
                    {
                        outlineComponent.enabled = false;
                    }
                    else
                    {
                        Debug.LogWarning("Previous weapon did not have an Outline component!");
                    }
                }

                // Assign and enable the outline on the new hovered weapon
                hoveredWeapon = weaponComponent;
                var newOutlineComponent = hoveredWeapon.GetComponent<Outline>();
                if (newOutlineComponent != null)
                {
                    newOutlineComponent.enabled = true;
                }
                else
                {
                    Debug.LogWarning("Hovered weapon does not have an Outline component!");
                }

                // Handle weapon pickup
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (WeaponManager.Instance != null)
                    {
                        WeaponManager.Instance.PickupWeapon(objectHitByRaycast);
                    }
                    else
                    {
                        Debug.LogError("WeaponManager.Instance is null!");
                    }
                }
            }
            else
            {
                // Disable the outline if it's not hovering a valid weapon
                if (hoveredWeapon != null)
                {
                    var outlineComponent = hoveredWeapon.GetComponent<Outline>();
                    if (outlineComponent != null)
                    {
                        outlineComponent.enabled = false;
                    }
                    else
                    {
                        Debug.LogWarning("Hovered weapon does not have an Outline component!");
                    }
                    hoveredWeapon = null;  // Reset hoveredWeapon
                }
            }
        }
        else
        {
            // Disable the outline if the raycast doesn't hit anything
            if (hoveredWeapon != null)
            {
                var outlineComponent = hoveredWeapon.GetComponent<Outline>();
                if (outlineComponent != null)
                {
                    outlineComponent.enabled = false;
                }
                else
                {
                    Debug.LogWarning("Hovered weapon does not have an Outline component!");
                }
                hoveredWeapon = null;
            }
        }
    }






    // private void Update()
    // {
    //    Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    //    RaycastHit hit;

    //   if (Physics.Raycast(ray, out hit))
    //    {
    //        GameObject objectHitByRaycast = hit.transform.gameObject;

    //     if (objectHitByRaycast.GetComponent<Weapon>() && objectHitByRaycast.GetComponent<Weapon>().isActiveWeapon == false)
    //      {
    //         hoveredWeapon = objectHitByRaycast.gameObject.GetComponent<Weapon>();
    //         hoveredWeapon.GetComponent<Outline>().enabled = true;

    //          if (Input.GetKeyDown(KeyCode.F))
    //         {
    //            WeaponManager.Instance.PickupWeapon(objectHitByRaycast.gameObject);
    //         }

    //     }
    //    else
    //    {
    //        if (hoveredWeapon)
    //        {
    //            hoveredWeapon.GetComponent<Outline>().enabled = false; 
    //        }
    //   }
    //  }

    // }

}
