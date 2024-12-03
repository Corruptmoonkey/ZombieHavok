using System;
using System.Collections;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }

    public WeaponScript hoveredWeapon = null;
    public ItemBox hoveredItemBox = null; // Track the currently hovered ItemBox
    private bool isItemBoxOpen = false; // Track if the item box is already open

    public GameObject[] weaponPrefabs; // Array of weapon prefabs to spawn
    public Transform weaponSpawnPoint; // Spawn point for weapons (set in the chest prefab)

    private GameObject currentWeapon; // Track the weapon you're holding (if any)
    private GameObject weaponInBox; // Track the weapon inside the box (if any)
    private bool wasWeaponInBoxPickedUp = false; // Track if the weapon in the box was picked up

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

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHitByRaycast = hit.transform.gameObject;

            // Handle Weapon interaction
            if (objectHitByRaycast.GetComponent<WeaponScript>() && !objectHitByRaycast.GetComponent<WeaponScript>().isActiveWeapon)
            {
                if (hoveredWeapon)
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                }

                hoveredWeapon = objectHitByRaycast.GetComponent<WeaponScript>();
                hoveredWeapon.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    PickUpWeapon();
                }
            }
            else
            {
                if (hoveredWeapon)
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                    hoveredWeapon = null;
                }
            }

            // Handle ItemBox interaction
            if (objectHitByRaycast.GetComponent<ItemBox>())
            {
                if (hoveredItemBox)
                {
                    hoveredItemBox.GetComponent<Outline>().enabled = false;
                }

                hoveredItemBox = objectHitByRaycast.GetComponent<ItemBox>();
                hoveredItemBox.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    OpenItemBox(objectHitByRaycast);
                }
            }
            else
            {
                if (hoveredItemBox)
                {
                    hoveredItemBox.GetComponent<Outline>().enabled = false;
                    hoveredItemBox = null;
                }
            }
        }
        else
        {
            if (hoveredWeapon)
            {
                hoveredWeapon.GetComponent<Outline>().enabled = false;
                hoveredWeapon = null;
            }

            if (hoveredItemBox)
            {
                hoveredItemBox.GetComponent<Outline>().enabled = false;
                hoveredItemBox = null;
            }
        }
    }

    private void PickUpWeapon()
    {
        if (hoveredWeapon == null) return;

        WeaponManager.Instance.PickUpWeapon(hoveredWeapon.gameObject);

        if (currentWeapon != null)
        {
            Destroy(currentWeapon); // Destroy the current weapon you're holding
        }

        currentWeapon = hoveredWeapon.gameObject; // Update the current weapon reference

        if (weaponInBox == hoveredWeapon.gameObject)
        {
            wasWeaponInBoxPickedUp = true; // Mark the box weapon as picked up
            weaponInBox = null;            // Clear reference to the box weapon
        }

        hoveredWeapon.GetComponent<Outline>().enabled = false;
        hoveredWeapon = null;
    }

    private void OpenItemBox(GameObject itemBox)
    {
        if (!isItemBoxOpen && PointsManager.Instance.points >= 1000)
        {
            Animator itemBoxAnimator = itemBox.GetComponent<Animator>();
            itemBoxAnimator.SetTrigger("OPEN");
            isItemBoxOpen = true;
            PointsManager.Instance.RemovePoints(1000);
            StartCoroutine(SpawnWeaponAfterDelay(2f, itemBox.transform));
            StartCoroutine(CloseBoxAfterDelay(itemBoxAnimator, 10f));
        }
        else
        {
            print("Not enough points");
        }
    }

    private IEnumerator SpawnWeaponAfterDelay(float delay, Transform chestTransform)
    {
        yield return new WaitForSeconds(delay);

        if (weaponPrefabs.Length > 0 && weaponSpawnPoint != null)
        {
            int randomIndex = UnityEngine.Random.Range(0, weaponPrefabs.Length);
            GameObject weapon = Instantiate(weaponPrefabs[randomIndex], weaponSpawnPoint.position, Quaternion.identity);
            weapon.transform.SetParent(chestTransform);
            weaponInBox = weapon; // Track the weapon in the box
            wasWeaponInBoxPickedUp = false; // Reset the pickup flag
        }
        else
        {
            print("No weapons or spawn point defined!");
        }
    }

    private IEnumerator CloseBoxAfterDelay(Animator itemBoxAnimator, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (weaponInBox != null && !wasWeaponInBoxPickedUp)
        {
            Destroy(weaponInBox);
            weaponInBox = null;
            print("Weapon in the box destroyed as it was not picked up." +  "Status: " + wasWeaponInBoxPickedUp);
        }

        if (itemBoxAnimator != null)
        {
            itemBoxAnimator.SetTrigger("CLOSE");
            isItemBoxOpen = false;
        }

        wasWeaponInBoxPickedUp = false; // Reset the flag after closing the box
    }
}
