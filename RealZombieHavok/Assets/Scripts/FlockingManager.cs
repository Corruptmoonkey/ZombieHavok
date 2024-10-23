using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{
    public static FlockingManager FM;

    public GameObject fishPrefab;
    public int numFish = 20;

    public GameObject[] allFish;

    public Vector3 swimLimit = new Vector3(5, 5, 5);

    public Vector3 goalPos = Vector3.zero;

    [Header("Zombie Settings")]
    [Range(0.0f, 5.0f)]
    public float minSpeed;
    [Range(0.0f, 5.0f)]
    public float maxSpeed;

    [Range(0.0f, 10.0f)]
    public float neighbourDistance;


    [Range(1.0f, 5.0f)]
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        allFish = new GameObject[numFish];
        for (int i = 0; i < numFish; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swimLimit.x, swimLimit.x), Random.Range(-swimLimit.y, swimLimit.y), Random.Range(-swimLimit.z, swimLimit.z));
            allFish[i] = Instantiate(fishPrefab, pos, Quaternion.identity);
        }
        FM = this;
        goalPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 100) < 10)
        {
            goalPos = this.transform.position + new Vector3(Random.Range(-swimLimit.x, swimLimit.x), Random.Range(-swimLimit.y, swimLimit.y), Random.Range(-swimLimit.z, swimLimit.z));


        }
    }
}
