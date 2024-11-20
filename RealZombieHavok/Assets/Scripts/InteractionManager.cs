using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }

    public WeaponScript hoveredWeapon = null;
    public ItemBox hoveredItemBox = null; // A variable to track the currently hovered ItemBox
    private bool isItemBoxOpen = false; // A flag to track if the item box is already open
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
            // Handle ItemBox interaction
            if (objectHitByRaycast.GetComponent<ItemBox>()) // New: Check if the raycast hits an ItemBox
            {
                if (hoveredItemBox) // New: Disable outline for the previously hovered ItemBox
                {
                    hoveredItemBox.GetComponent<Outline>().enabled = false;
                }

                print("Press F to open");
                hoveredItemBox = objectHitByRaycast.GetComponent<ItemBox>(); // New: Update hoveredItemBox reference
                hoveredItemBox.GetComponent<Outline>().enabled = true; // New: Enable outline for the currently hovered ItemBox

                if (Input.GetKeyDown(KeyCode.F) && !isItemBoxOpen)
                {
                    // Get the Animator component and trigger the animation
                    Animator itemBoxAnimator = objectHitByRaycast.GetComponent<Animator>();

                   
                    itemBoxAnimator.SetTrigger("OPEN");
                    isItemBoxOpen = true;
                    print("Opening the item box...");
                    Coroutine coroutine = StartCoroutine(CloseBoxAfterDelay(itemBoxAnimator, 10));

                }
            }
            else
            {
                if (hoveredItemBox) // New: Reset the outline if the raycast moves away from the ItemBox
                {
                    hoveredItemBox.GetComponent<Outline>().enabled = false;
                    hoveredItemBox = null; // New: Clear the hoveredItemBox reference
                }
            }
        }
        else
        {
            // Reset Weapon outline if raycast stops hitting
            if (hoveredWeapon)
            {
                hoveredWeapon.GetComponent<Outline>().enabled = false;
                hoveredWeapon = null;
            }

            // Reset ItemBox outline if raycast stops hitting
            if (hoveredItemBox) // New: Reset the outline and reference for the ItemBox
            {
                hoveredItemBox.GetComponent<Outline>().enabled = false;
                hoveredItemBox = null; // New: Clear the hoveredItemBox reference
            }
        }
    }

    private IEnumerator CloseBoxAfterDelay(Animator itemBoxAnimator, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (itemBoxAnimator != null)
        {
            itemBoxAnimator.SetTrigger("CLOSE");
            isItemBoxOpen = false;
            print("Closing the item box...");
        }
    }
}