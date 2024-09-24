using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void OnCollisionEnter(Collision objectWeHit)
    {
        if(objectWeHit.gameObject.CompareTag("Target"))
        {
            print("hit " + objectWeHit.gameObject.name + " !");

            Destroy(gameObject);
        }

        if(objectWeHit.gameObject.CompareTag("Wall"))
        {
            print("hit a wall");

            Destroy(gameObject);
        }
    }

}
