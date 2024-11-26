using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    public static int HP = 100;
    public int MaxHP = 100;
   
    public bool isDead;

    public MonoBehaviour cameraControlScript; // Reference to the Camer's Script
    public Animator cameraAnimator; // Reference to the camera's Animator component
    
    public TextMeshProUGUI playerHealthUI;

    public GameObject gameOverUI;
    public Slider SliderPlayerHealth;

    public void Start()
    {
        //HP is now displayed by this Slider by Steven Pichelman
        SliderPlayerHealth.value = HP;
    }


    public void TakeDamage(int damageAmount)
    {
        // HP difficulty modifier by Steven Pichelman
        HP -= Mathf.RoundToInt(damageAmount * PlayerPrefs.GetFloat("Difficulty", 1f));
        if (HP <= 0)
        {
            print("Player Dead");
            PlayerDead();
            isDead = true;
         
        }
        else
        {
            print("Player Hit");
            SliderPlayerHealth.value = HP;


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
        //Health drop & ammo functionality by Steven Pichelman
        else if (other.CompareTag("HealthDrop"))
        {
            if (HP == MaxHP) //do not consume drop if full health
            {
                return;
            }
            HP += other.GetComponent<HealthDrop>().HealthAmount;
            if (HP > MaxHP)
            {
                HP = MaxHP;
            }
            SliderPlayerHealth.value = HP;
            Debug.Log($"Healed to {HP} HP");
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("AmmoDrop"))
        {
            AmmoManager.Instance.AddAmmo(other);
        }
    }

    private void PlayerDead()
    {
        SliderPlayerHealth.value = 0; 
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

