using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{

    [SerializeField] private int HP = 100;
    private Animator animator;


    private NavMeshAgent navAgent;
    // Start is called before the first frame update
    void Start()
    {
        // Gets the animator and Nav agent components associated with the script's object
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }



    public  void TakeDamage(int damageAmount)
    {

        HP -= damageAmount;

        if(HP <= 0)
        {
            int randomValue = Random.Range(0,2); // 0 or 1

            if (randomValue == 0)
            {
                animator.SetTrigger("DIE1");
            }
            else
            {
                animator.SetTrigger("DIE2");
            }
          
        }

        else
        {
           animator.SetTrigger("DAMAGE");
        }
    }

    public int getHP()
    {
        return HP;
    }

 
}