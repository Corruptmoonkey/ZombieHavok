/*
SpawnDrop
by Steven Pichelman
11/9/2024
spawns drops around the map in accordance with round/time.
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnDrop : MonoBehaviour
{
    public GameObject[] AmmoDrops;
    public GameObject[] HealthDrops;
    public GameObject[] WeaponDrops;
    public GameObject[] DropsSpawned;
    public List<Transform> SpawnLocations;
    public int HealthDropsToSpawn;
    public int AmmoDropsToSpawn;
    public int WeaponDropsToSpawn;
    private int HealthDropsCount;
    private int AmmoDropsCount;
    private int WeaponDropsCount;
    private void Start()
    {
        ResetDropCounts();
        SpawnRandom();
    }
    //this is called by ZombieSpawner at beginning of each round. 
    public void SpawnRandom()
    {
        if (Player.HP == 100) //give no health drops
        {
            //give drop amount instead to ammo and weapons
            for (int i = 0; i < HealthDropsCount; i+=2)
            {
                PlaceDrop(AmmoDrops, 1);
                PlaceDrop(WeaponDrops, 1); ;
            }
        }
        else
        {
            PlaceDrop(HealthDrops, HealthDropsCount);
        }
        // no more checks to do as ammo and weapons are not finalized
        PlaceDrop(AmmoDrops, AmmoDropsCount);
        PlaceDrop(WeaponDrops, WeaponDropsCount);
    }

    private void ResetDropCounts()
    {
        //reset drops remaining to default values
        HealthDropsCount = HealthDropsToSpawn;
        AmmoDropsCount = AmmoDropsToSpawn;
        WeaponDropsCount = WeaponDropsToSpawn;
    }

    private void PlaceDrop(GameObject[] Drops, int Counter)
    {
        if (Drops.Length == 0) return;
        for (int i = 0; i < Counter; i++)
        {
        GameObject PickedDrop = Drops[Random.Range(0, Drops.Length - 1)];
            //add some variance around where the drop spawns
            Vector3 RandomLocation = SpawnLocations[Random.Range(0, SpawnLocations.Count)].position;
            Vector3 PickedLocation = new (RandomLocation.x + Random.Range(-3f,3f), 0.5f, RandomLocation.z + Random.Range(-3f, 3f));

             GameObject aDrop = Instantiate(PickedDrop, PickedLocation,Quaternion.identity);
         // keep track of these drops
         DropsSpawned.Append(aDrop);
        }
    }
    private void Update()
    { //temp
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SpawnRandom();
        }
    }
}
