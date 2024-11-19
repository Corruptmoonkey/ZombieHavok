using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }

    public WeaponScript hoveredWeapon = null;
    private void Awake()
    {

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

    }
    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHitByRaycast = hit.transform.gameObject;
            if (objectHitByRaycast.GetComponent<WeaponScript>() && objectHitByRaycast.GetComponent<WeaponScript>().isActiveWeapon == false)
            {
                if (hoveredWeapon)
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                }
                print("Press F to equip");
                hoveredWeapon = objectHitByRaycast.gameObject.GetComponent<WeaponScript>();
                hoveredWeapon.GetComponent<Outline>().enabled = true;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.Instance.PickUpWeapon(objectHitByRaycast.gameObject);
                }
            }
            else
            {
                if (hoveredWeapon)
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                }


            }
            if (objectHitByRaycast.GetComponent<ItemBox>()){
                print("Press F to open");
                if (Input.GetKeyDown(KeyCode.F))
                {
                    // Get the Animator component and trigger the animation
                    Animator itemBoxAnimator = objectHitByRaycast.GetComponent<Animator>();

                    if (itemBoxAnimator != null)
                    {
                        itemBoxAnimator.SetTrigger("OPEN");
                        print("Opening the item box...");
                    }
                    else
                    {
                        print("No Animator found on ItemBox!");
                    }
                }
            }

        }


        else
        {
            if (hoveredWeapon)
            {
                hoveredWeapon.GetComponent<Outline>().enabled = false;
            }

        }
    }
}