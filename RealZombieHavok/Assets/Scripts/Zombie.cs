// Written by Jay Gunderson
// 10/14/2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public ZombieHand zombieHand;
    public int zombieDamage;


    private void Start()
    {
        zombieHand.damage = zombieDamage;
    }
}
