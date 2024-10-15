using UnityEngine;
using System.Collections;

public class Firearm : MonoBehaviour
{
    public int maxAmmo = 10;         // Maximum ammo for the weapon
    public int currentAmmo;          // Current ammo count
    public float reloadTime = 1.5f;  // Reload time in seconds
    public float range = 100f;       // Range of the weapon
    public int damage = 20;          // Damage dealt by the weapon
    public AudioSource gunshotSound; // Sound effect for shooting
    public Camera fpsCam;            // First-person camera

    private bool isReloading = false;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (isReloading) return;

        if (Input.GetButtonDown("Fire1"))  // Left mouse button
        {
            if (currentAmmo > 0)
            {
                Shoot();
            }
            else
            {
                StartCoroutine(Reload());
            }
        }

        if (Input.GetKeyDown(KeyCode.R))  // Press 'R' to reload manually
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        gunshotSound.Play();  // Play the gunshot sound
        currentAmmo--;         // Decrease ammo count

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);

            // If the hit object has health, apply damage
            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
}
