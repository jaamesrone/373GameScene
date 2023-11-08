using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionWithHead : MonoBehaviour
{
    public GameObject head; 
    public float popForce = 10f; 
    public float fallDelay = 1f; 
    public AudioClip detachSound; 
    private bool canDetachHead = false;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (canDetachHead && Input.GetKeyDown(KeyCode.E))
        {
            DetachHead();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            canDetachHead = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canDetachHead = false;
        }
    }

    private void DetachHead()
    {
        // Play the detach sound
        if (audioSource != null && detachSound != null)
        {
            audioSource.PlayOneShot(detachSound);
        }

        // Detach the head from the body
        head.transform.parent = null;

        // Apply a force to "pop up" the head
        Rigidbody headRigidbody = head.GetComponent<Rigidbody>();
        if (headRigidbody != null)
        {
            headRigidbody.isKinematic = false; // Allow physics to affect the head
            headRigidbody.AddForce(Vector3.up * popForce, ForceMode.Impulse);
        }

        // Add a delay before the head falls (you can use a Coroutine for this)
        Invoke("FallHead", fallDelay);
    }

    private void FallHead()
    {
        Rigidbody headRigidbody = head.GetComponent<Rigidbody>();
        if (headRigidbody != null)
        {
            headRigidbody.useGravity = true; // Enable gravity to make the head fall
        }
    }
}
