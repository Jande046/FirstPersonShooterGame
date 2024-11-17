using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    public float fireRate = 0.5f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 20f;
    private float nextFireTime = 0f;

public void Shoot()
{
    if(Time.time >= nextFireTime)
    {
        if(projectilePrefab != null)
        {
            FireProjectile();
        }
    
        else
        {
            Debug.Log("No projectile assigned.");
        }

    nextFireTime = Time.time + fireRate;
    }
    else{
        //Debug.Log("Weapon is cooling down");
    }
}

private void FireProjectile()
{
    if (firePoint == null)
        {
            Debug.LogError("FirePoint is not assigned!");
            return;
        }

        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Add force to the projectile (assuming it has a Rigidbody component)
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * projectileSpeed;
        }

        //Debug.Log("Projectile Fired!");
}
}