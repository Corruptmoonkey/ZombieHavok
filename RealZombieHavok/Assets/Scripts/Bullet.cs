// Edited by Jay Gunderson
// 10/07/2024

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage;
    private void OnCollisionEnter(Collision objectWeHit)
    {



        if(objectWeHit.gameObject.CompareTag("Target"))
        {
            print("hit " + objectWeHit.gameObject.name + " !");

            Destroy(gameObject); // Destroys  buillet
        }

        if(objectWeHit.gameObject.CompareTag("Wall"))
        {
            print("hit a wall");

            Destroy(gameObject);// Destroys  buillet
        }

        if (objectWeHit.gameObject.CompareTag("Enemy"))
        {

            if (objectWeHit.gameObject.GetComponent<Enemy>().isDead == false)
            {
                objectWeHit.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage); // Stops from having the enemy having negative health and stops the animation from looping

            }

            print("You hit a zombie!" + " HP remaining: " + objectWeHit.gameObject.GetComponent<Enemy>().getHP());
           

            CreateBloodSpreadEffect(objectWeHit);
            Destroy(gameObject);// Destroys  buillet

        }
    }

    private void CreateBloodSpreadEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];

        GameObject bloodSprayPrefab = Instantiate(GlobalReferences.Instance.bloodSprayEffect, contact.point, Quaternion.LookRotation(contact.normal));

        bloodSprayPrefab.transform.SetParent(objectWeHit.gameObject.transform);
        Destroy(gameObject);
    }
}
