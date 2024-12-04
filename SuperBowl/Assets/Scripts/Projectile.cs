using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public float lifetime = 5f; // Time in seconds before the projectile disappears
    public float projectileDamage = 10f;

    void Start()
    {
        // Destroy the projectile after the specified lifetime
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
{
    Debug.Log("Projectile collided with: " + collision.gameObject.name + ", Tag: " + collision.gameObject.tag);

    if (collision.gameObject.CompareTag("Enemy"))
    {
        // Damage logic for the enemy
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(projectileDamage);
            Debug.Log("Enemy hit. Damage applied: " + projectileDamage);
        }
    }
    else if (collision.gameObject.CompareTag("Player"))
    {
        // Ignore the player
        Debug.Log("Projectile hit the player, ignoring...");
    }
    else
    {
        Debug.Log("Projectile hit non-enemy: " + collision.gameObject.name);
    }

    // Destroy the projectile itself, not the collided object
    Destroy(gameObject);
}

}
