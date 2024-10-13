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

        if (objectWeHit.gameObject.CompareTag("Zombie"))
        {
            
            objectWeHit.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage);
            print("You hit a zombie!" + " HP remaining: " + objectWeHit.gameObject.GetComponent<Enemy>().getHP());
            Destroy(gameObject);// Destroys  buillet
        }
    }

}
