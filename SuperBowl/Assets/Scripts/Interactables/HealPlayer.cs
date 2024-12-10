using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : Interactable
{
     [Header("Health Cube Settings")]
    public float healAmount = 20f;         // Amount of health to heal
    public float respawnDelay = 5f;        // Time before the cube respawns
    public Transform[] respawnLocations;   // Array of possible respawn locations

    private Renderer cubeRenderer;
    private Collider cubeCollider;

    private void Start()
    {
        // Cache references to the renderer and collider for enabling/disabling
        cubeRenderer = GetComponent<Renderer>();
        cubeCollider = GetComponent<Collider>();
    }

    protected override void Interact()
    {
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            // Heal the player
            playerHealth.RestoreHealth(healAmount);

            // Disable the cube and start the respawn process
            StartCoroutine(RespawnHealthCube());
        }
    }

    private IEnumerator RespawnHealthCube()
    {
        // Disable the cube
        cubeRenderer.enabled = false;
        cubeCollider.enabled = false;

        // Wait for the respawn delay
        yield return new WaitForSeconds(respawnDelay);

        // Choose a random respawn location
        Transform newLocation = respawnLocations[Random.Range(0, respawnLocations.Length)];
        transform.position = newLocation.position;

        // Re-enable the cube
        cubeRenderer.enabled = true;
        cubeCollider.enabled = true;
    }
}
