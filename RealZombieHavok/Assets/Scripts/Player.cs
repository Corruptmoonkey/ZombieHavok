using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int HP = 100;
   
    public bool isDead;

    public MonoBehaviour cameraControlScript; // Reference to the Camer's Script
    public Animator cameraAnimator; // Reference to the camera's Animator component
    
    public TextMeshProUGUI playerHealthUI;

    public GameObject gameOverUI;


    public void Start()
    {
        playerHealthUI.text = $"Health: {HP}";
    }


    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0)
        {
            print("Player Dead");
            PlayerDead();
            isDead = true;
         
        }
        else
        {
            print("Player Hit");
            playerHealthUI.text = $"Health: {HP}";


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

    private void PlayerDead()
    {
        GetComponent<PlayerMovement>().enabled = false; // Movement is disabled
        cameraControlScript.enabled = false; // Camera controls are disabled
        cameraAnimator.enabled = true;
        playerHealthUI.gameObject.SetActive(false);
        GetComponent<Blackout>().StartFade();
        StartCoroutine(ShowGameOverUI());
    }

    private IEnumerator ShowGameOverUI()
    {
        yield return new WaitForSeconds(1f); // Delay
        gameOverUI.gameObject.SetActive(true);

    }
}

