using UnityEngine;

public class melee : MonoBehaviour
{
    public int damage = 30;           // Damage dealt by the knife
    public float attackRange = 1.5f;  // Range of the attack
    public float attackCooldown = 1.0f; // Time between attacks
    public LayerMask enemyLayer;      // Layer for detecting enemies
    public Animator animator;         // Reference to Animator for attack animation
    public AudioSource swingSound;    // Sound for knife swing

    private float nextAttackTime = 0f; // Track when the player can attack again

    void Update()
    {
        if (Time.time >= nextAttackTime && Input.GetButtonDown("Fire1")) // Left mouse button
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown; // Set the next available attack time
        }
    }

    void Attack()
    {
        // Play swing animation and sound
        animator.SetTrigger("Attack");
        swingSound.Play();

        // Perform a raycast to detect enemies within range
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, enemyLayer))
        {
            Debug.Log("Hit: " + hit.transform.name);

        }
    }
}
