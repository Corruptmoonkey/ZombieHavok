using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int HP = 100;
   
    public bool isDead;

 
    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0)
        {
            print("Player Dead");
       
            isDead = true;
         
        }
        else
        {
            print("Player Hit");
          
            
            
        }
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZombieHand"))
        {
            if (!isDead)
            {
                TakeDamage(other.gameObject.GetComponent<ZombieHand>().damage);
            }


        }
    }
}

