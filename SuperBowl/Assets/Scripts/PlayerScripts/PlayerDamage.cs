using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
 /*   public float damageFromEnemy = 20f; // Set the damage the player will take from enemies

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collided with an object tagged as "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Get the PlayerHealth component and apply damage
            PlayerHealth playerHealth = GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageFromEnemy); // Call the TakeDamage method in the PlayerHealth script
            }
        }
    }
    */
    public void ApplyDamage(float damageAmount)
    {
        // Find the PlayerHealth component and apply the damage
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount); // Apply the damage
        }
    }
}
