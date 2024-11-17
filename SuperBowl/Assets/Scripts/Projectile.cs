using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public float lifetime = 5f; // Time in seconds before the projectile disappears
    public float projectileDamage = 10f; // Damage the projectile deals to the enemy

    void Start()
    {
        // Destroy the projectile after the specified lifetime
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the projectile collides with an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Get the Health component of the enemy and apply damage
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(projectileDamage); // Apply damage to enemy
                Debug.Log("Enemy hit! Damage applied: " + projectileDamage);
            }
            else
            {
                Debug.LogWarning("Enemy does not have an EnemyHealth component.");
            }

            // Destroy the projectile after it hits the enemy
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Projectile hit non-enemy: " + collision.gameObject.name);
        }
    }
}
