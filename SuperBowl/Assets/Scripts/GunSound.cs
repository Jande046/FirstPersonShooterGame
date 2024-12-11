using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSound : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip gunshotSound; // Drag and drop your gunshot sound effect here

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Ensure an AudioClip is assigned
        if (gunshotSound == null)
        {
            Debug.LogError("Gunshot sound not assigned in the inspector!");
        }
    }

    // Method to play the gunshot sound
    public void PlayGunshotSound()
    {
        if (gunshotSound != null)
        {
            audioSource.PlayOneShot(gunshotSound); // Play the sound
        }
    }
}

