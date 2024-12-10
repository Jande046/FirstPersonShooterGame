using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 0.5f;
    public Camera fpsCam;

   public float sphereRadius = 0.5f; // Adjust the radius as needed
    

    

    public float nextFireTime = 0f;

    
    

    public void Shoot()
    {
       // Visualize SphereCast for debugging
    Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * range, Color.red, 1f);

    // Define sphere radius and origin
    
    Vector3 rayOrigin = fpsCam.transform.position + fpsCam.transform.forward * 0.1f;

    // Perform SphereCast
    if (Physics.SphereCast(rayOrigin, sphereRadius, fpsCam.transform.forward, out RaycastHit hitInfo, range))
    {
        Debug.Log($"Hit: {hitInfo.transform.name}");

        // Check if the hit object has the "Enemy" tag
        if (hitInfo.transform.CompareTag("Enemy"))
        {
            // Handle enemy health
            EnemyHealth enemyHealth = hitInfo.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }

        // Apply impact force to objects with a rigidbody
        if (hitInfo.rigidbody != null && !hitInfo.rigidbody.isKinematic)
        {
            hitInfo.rigidbody.AddForce(-hitInfo.normal * impactForce, ForceMode.Impulse);
        }
    }
    
        
       /*RaycastHit hitInfo;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo, range))
        {
            Debug.Log(hitInfo.transform.name);
            EnemyHealth  enemyHealth = hitInfo.transform.GetComponent<EnemyHealth>();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            if(hitInfo.rigidbody != null)
            {
                hitInfo.rigidbody.AddForce(-hitInfo.normal * impactForce); 
            }
        }

    }*/
    }
}
