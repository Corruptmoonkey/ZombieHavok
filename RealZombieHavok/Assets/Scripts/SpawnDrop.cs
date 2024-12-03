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
using Unity.VisualScripting;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnDrop : MonoBehaviour
{
    public GameObject[] AmmoDrops;
    public GameObject[] HealthDrops;
    public GameObject[] WeaponDrops;
    public List<GameObject> AmmoDropsSpawned = new List<GameObject>();
    public List<GameObject> HealthDropsSpawned = new List<GameObject>();
    public List<GameObject> WeaponDropsSpawned = new List<GameObject>();
    public List<Transform> SpawnLocations;
    private Vector3 PickedRotation;
    public int HealthDropsToSpawn;
    public int AmmoDropsToSpawn;
    public int WeaponDropsToSpawn;
    private int HealthDropsCount;
    private int AmmoDropsCount;
    private int WeaponDropsCount;
    private void Start()
    {
        ResetDropCounts();

    }
    //this is called by ZombieSpawner at beginning of each round. 
    public void SpawnRandom()
    {
        if (Player.HP == 100) //give no health drops
        {
            //give drop amount instead to ammo and weapons
            for (int i = 0; i < HealthDropsCount; i+=2)
            {
                PlaceDrop(AmmoDrops, 1, ref AmmoDropsSpawned);
                PlaceDrop(WeaponDrops, 1, ref WeaponDropsSpawned);
            }
        }
        else
        {
            PlaceDrop(HealthDrops, HealthDropsCount, ref HealthDropsSpawned);
        }

        // no more checks to do as ammo and weapons are not finalized
        PlaceDrop(AmmoDrops, AmmoDropsCount, ref AmmoDropsSpawned);
        PlaceDrop(WeaponDrops, WeaponDropsCount, ref WeaponDropsSpawned);

        Debug.Log($"{AmmoDropsSpawned.Count} Ammo Spawned, {HealthDropsSpawned.Count}  Health Spawned, {WeaponDropsSpawned.Count} Weapons Spawned");
    }

    private void ResetDropCounts()
    {
        //reset drops remaining to default values
        HealthDropsCount = HealthDropsToSpawn;
        AmmoDropsCount = AmmoDropsToSpawn;
        WeaponDropsCount = WeaponDropsToSpawn;
    }

    private void PlaceDrop(GameObject[] Drops, int Counter, ref List<GameObject> DropsSpawned)
    {
        if (Drops.Length == 0) return; // return if no objects to spawn is specified

        // this caps number of drops in map for given drop type
        Counter -= DropsSpawned.Count;
        if (Counter <= 0) return;

        for (int i = 0; i < Counter; i++)
        {
             GameObject PickedDrop = Drops[Random.Range(0, Drops.Length)]; //do not modify PickedDrop as it is just the prefab

            //set spawn rotation. this is different for each object. y axis is random.
            Vector3 PickedRotation;
            if (PickedDrop.TryGetComponent(out WeaponScript aWeaponScript))
            { //pull default rotation from weapon spawn script
                PickedRotation = new Vector3(aWeaponScript.spawnRotation.x, Random.Range(0, 350), aWeaponScript.spawnRotation.z + 90);
            }
            else
            { //default rotation for all other drops (not weapons)
                PickedRotation = new Vector3(0, Random.Range(0, 350),0);
            }

            //deternime the y axis offset for how tall the object is when spawned in. this would be easier if these objects had physics.
            //offset from gound is equal to y axis of BoxCollider / 2 * scale of y axis; this is distance from center to ground.
            float YAxis = 0.06f; //generally good default for weapons
            if (PickedDrop.TryGetComponent(out BoxCollider aBoxCollider))
            {
                YAxis = aBoxCollider.size.y/2 * PickedDrop.transform.localScale.y; //this should be correct offset if center of BoxCollider is in center.
            }

            //add some variance around where the drop spawns. currently can overlap each other.
            Vector3 RandomLocation = SpawnLocations[Random.Range(0, SpawnLocations.Count)].position;
            RandomLocation = new Vector3(RandomLocation.x + Random.Range(-3f, 3f), YAxis, RandomLocation.z + Random.Range(-3f, 3f));

            GameObject aDrop = Instantiate(PickedDrop, RandomLocation, Quaternion.Euler(PickedRotation));

            // keep track of these drops (per category)
            DropsSpawned.Add(aDrop);
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
//todo: snap to floor. 0.06 for ak. cant do from constructr as same for ak vs 1911.
//also rotate correctly
//also do ammo
//fix gun collision & bull et collision