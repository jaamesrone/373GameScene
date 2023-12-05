using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    public static int totalMannequins; // Static variable to store the total number of mannequins
    public static int interactedMannequins; // Static variable to count the number of interacted mannequins

    public Transform head;
    public Transform body;
    public float interactDistance = 2f;
    public KeyCode interactKey = KeyCode.E;
    public AudioClip popSound;

    // UI Text
    public Text displayText;
    public Text SpecialEventText;

    public GameObject RainFall;
    public GameObject Clouds;
    public GameObject directionalLightObject;

    private bool canInteract = false;
    private bool hasInteracted = false;

    private ConfigurableJoint headJoint;
    private ConfigurableJoint bodyJoint;
    private AudioSource audioSource;

    private int interactionCount = 0;
    public int interactionThreshold;

    private void Start()
    {
        // Increment the total number of mannequins when a new mannequin is instantiated
        totalMannequins++;

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
            displayText.enabled = canInteract && !hasInteracted; // Show text only if not interacted and near the mannequin
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

        // Set the flag to indicate that the interaction has occurred
        hasInteracted = true;

        // Increment the interacted mannequins count
        interactedMannequins++;

        // Check if all mannequins have been interacted with
        if (interactedMannequins >= totalMannequins)
        {
            // Trigger the specific code for all mannequins interacted
            AllMannequinsInteracted();
        }
    }

    private void AllMannequinsInteracted()
    {
        // Your specific code for when all mannequins have been interacted
        Debug.Log("All mannequins interacted!");

        // ... (perform other actions)
    }

    private void LightSensitivity(float intensity)
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

    private IEnumerator TurnOffEffects(float duration)
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