// Written by Jay Gunderson
// 10/06/2024
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    private NavMeshAgent navAgent; // Gets Nav agent object

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            print("The enemy is moving where you cliked!");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            if(Physics.Raycast(ray, out hit, Mathf.Infinity, NavMesh.AllAreas)) // If the player clicks the ground
            {
                navAgent.SetDestination(hit.point); // Moves agent on the position clicked
            }
        }
    }
}
