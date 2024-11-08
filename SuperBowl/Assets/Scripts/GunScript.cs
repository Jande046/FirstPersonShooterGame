using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    [Header("Gun Settings")]
    public int maxAmmo = 30;        // Maximum ammo per magazine
    public int currentAmmo;          // Current ammo in the magazine
    public float fireRate = 0.1f;    // Time between each shot
    public float reloadTime = 1.5f;  // Time taken to reload

    [Header("References")]
    public AudioSource gunAudioSource;   // AudioSource component for gun sounds
    public AudioClip fireSound;          // Sound to play when firing
    public AudioClip reloadSound;        // Sound to play when reloading
    public ParticleSystem muzzleFlash;   // Muzzle flash effect

    private bool isReloading = false;
    private float nextTimeToFire = 0f;

    void Start()
    {
        currentAmmo = maxAmmo;  // Set ammo to max at start
    }

    void Update()
    {
        if (isReloading) return;  // Don't allow firing while reloading

        // Handle firing
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            Fire();
        }

        // Handle reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    void Fire()
    {
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        // Play fire sound and muzzle flash
        gunAudioSource.PlayOneShot(fireSound);
        muzzleFlash.Play();

        // Logic for firing (e.g., raycasting, spawning bullets)
        // Implement your firing logic here, such as shooting a ray from the gun

        currentAmmo--;  // Decrease ammo count
    }

    IEnumerator Reload()
    {
        isReloading = true;

        // Play reload sound
        gunAudioSource.PlayOneShot(reloadSound);

        // Wait for reload duration
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;  // Reset ammo to max
        isReloading = false;
    }
}
