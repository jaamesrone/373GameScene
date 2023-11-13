using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public Transform head;
    public Transform body;
    public float interactDistance = 2f;
    public KeyCode interactKey = KeyCode.E;
    public AudioClip popSound; // Add your pop sound effect here

    private bool canInteract = false;
    private bool hasInteracted = false;

    private ConfigurableJoint joint;
    private AudioSource audioSource;

    private void Start()
    {
        // Create a ConfigurableJoint on the head
        joint = head.gameObject.AddComponent<ConfigurableJoint>();
        joint.connectedBody = body.GetComponent<Rigidbody>();
        joint.enableCollision = false; // Disable collision between head and body
        joint.breakForce = Mathf.Infinity; // Make the joint unbreakable initially

        // Add an AudioSource for sound effects
        audioSource = head.gameObject.AddComponent<AudioSource>();
        audioSource.clip = popSound;
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        // Check if the interaction has already occurred
        if (hasInteracted)
        {
            return; // Exit the update loop if already interacted
        }

        // Check if the player is within the interaction distance
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        canInteract = distance < interactDistance;

        // Display UI prompt
        if (canInteract)
        {
            // Implement your UI display logic here
            Debug.Log("Press 'E' to interact");
        }

        // Check for player input to interact
        if (canInteract && Input.GetKeyDown(interactKey))
        {
            PerformInteraction();
        }
    }

    private void PerformInteraction()
    {
        // Break the joint to separate the head from the body
        Destroy(joint);

        // Play the sound effect
        if (audioSource != null && popSound != null)
        {
            audioSource.PlayOneShot(popSound);
        }

        // Apply force to make the head pop up
        Rigidbody headRb = head.GetComponent<Rigidbody>();
        if (headRb != null)
        {
            headRb.isKinematic = false;
            headRb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }

        // Perform any other actions you want here


        // Set the flag to indicate that the interaction has occurred
        hasInteracted = true;
    }

}
