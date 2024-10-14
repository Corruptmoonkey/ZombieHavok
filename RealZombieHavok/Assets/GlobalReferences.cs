// Written by Jay Gunderson
// 10/14/2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalReferences : MonoBehaviour
{
    public static GlobalReferences Instance { get; set; }
    public GameObject bloodSprayEffect;
    
    private void Awake()
    {

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

    }
}
