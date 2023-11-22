using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    public Transform head;
    public Transform body;
    public float interactDistance = 2f;
    public KeyCode interactKey = KeyCode.E;
    public AudioClip popSound; // Add your pop sound effect here

    // UI Text
    public Transform player;
    public Text displayText;
    public Text SpecialEventText;
    public float displayDistance = 5f;

    public GameObject RainFall;
    public GameObject Clouds;
    public GameObject directionalLightObject;

    private bool canInteract = false;
    private bool hasInteracted = false;

    private ConfigurableJoint headJoint;
    private ConfigurableJoint bodyJoint;
    private AudioSource audioSource;





    //Based on how many heads are popped up

    private int interactionCount = 0; // New variable to count interactions
    public int interactionThreshold; //set it in the hierachy


    private void Start()
    {
        // Create a ConfigurableJoint on the head
        headJoint = head.gameObject.AddComponent<ConfigurableJoint>();
        headJoint.connectedBody = body.GetComponent<Rigidbody>();
        headJoint.enableCollision = false; // Disable collision between head and body
        headJoint.breakForce = Mathf.Infinity; // Make the joint unbreakable initially

        // Create a ConfigurableJoint on the body
        bodyJoint = body.gameObject.AddComponent<ConfigurableJoint>();
        bodyJoint.connectedBody = head.GetComponent<Rigidbody>();
        bodyJoint.enableCollision = false; // Disable collision between body and head
        bodyJoint.breakForce = Mathf.Infinity; // Make the joint unbreakable initially

        // Add an AudioSource for sound effects
        audioSource = head.gameObject.AddComponent<AudioSource>();
        audioSource.clip = popSound;
        audioSource.playOnAwake = false;

        // UI Text initialization
        if (displayText != null)
        {
            displayText.enabled = false; // Hide the text initially
        }
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

        // Display or hide the UI text based on distance
        if (displayText != null)
        {
            displayText.enabled = canInteract && !hasInteracted; // Show text only if not interacted
        }

        // Check for player input to interact
        if (canInteract && Input.GetKeyDown(interactKey))
        {
            PerformInteraction();
        }
    }

    private void PerformInteraction()
    {
        // Hide the UI text
        if (displayText != null)
        {
            displayText.enabled = false;
        }

        // Break the joint on the head to separate it from the body
        Destroy(headJoint);

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

         interactionCount++;

        // Check if the interaction count has reached the threshold
        if (interactionCount >= interactionThreshold)
        {
            SpecialEvent();
        }

        // Perform any other actions you want here

        // Set the flag to indicate that the interaction has occurred
        hasInteracted = true;
    
    }

    private void SpecialEvent() //james' code
    {
        // turn on particle effects 
        if (RainFall != null)
        {
            RainFall.SetActive(true);
        }

        if (Clouds != null)
        {
            Clouds.SetActive(true);
        }

        if (SpecialEventText != null)
        {
            SpecialEventText.text = "Special Code Activated!";
        }

        // changes the intensity lighting from 2 to .3
        if (directionalLightObject != null)
        {
            LightSensitivity(0.3f);
        }

        // starts a 180 second timer to toggle the rain, rain, and intensity
        StartCoroutine(TurnOffEffects(180f)); // 180 seconds = 3 minutes
        Debug.Log("Special code activated!");
    }

    private void LightSensitivity(float intensity) //james' code
    {
        if (directionalLightObject != null)
        {
            Light directionalLight = directionalLightObject.GetComponent<Light>();
            if (directionalLight != null)
            {
                directionalLight.intensity = intensity;
            }
        }
    }

    private IEnumerator TurnOffEffects(float duration) //james' code
    {
        yield return new WaitForSeconds(duration);

        // after 180 seconds the game objects turn back off. :)
        if (RainFall != null)
        {
            RainFall.SetActive(false);
        }

        else if (Clouds != null)
        {
            Clouds.SetActive(false);
        }
        else if (directionalLightObject != null)
        {
            LightSensitivity(2f);
        }
    }

}
