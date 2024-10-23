using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public Camera playerCamera;

    // Shooting
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    // Burst
    public int bulletsPerBurst = 3;
    public int currentBurst;

    // Spread
    public float spreadIntensity;

    //Bullets
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletPrefabLifeTime = 3f;

    public enum ShootingMode{
        Single,
        Burst,
        Auto
    }
    public ShootingMode currentShootingMode;

    // Start is called before the first frame update
    void Awake()
    {
        readyToShoot = true;
        currentBurst = bulletsPerBurst;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentShootingMode == ShootingMode.Auto)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if(readyToShoot && isShooting)
        {
            currentBurst = bulletsPerBurst;
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;



        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        bullet.transform.forward = shootingDirection;

        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));
    }

    private Vector3 CalculateDirectionAndSpread()
    {
        throw new NotImplementedException();
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
       
    }
}
