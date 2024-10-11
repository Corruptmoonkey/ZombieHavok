using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{

    [SerializeField] private int HP = 100;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }



    public  void TakeDamage(int damageAmount)
    {

        HP -= damageAmount;

        if(HP <= 0)
        {
           // animator.SetTrigger("DIE");
            print("The zombie is dead");
            Destroy(gameObject); // Removes the zombie from game
        }

        else
        {
          //  animator.SetTrigger("DAMAGE");
        }
    }

    public int getHP()
    {
        return HP;
    }
}
