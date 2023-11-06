using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionWithHead : MonoBehaviour
{
    public float interactionDistance = 2f; // Adjust the distance at which the player can interact
    public GameObject interactionUI;
    public Text interactionText;

    void Start()
    {
        // Make sure the interaction UI is initially hidden
        interactionUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // You can change the key to your desired interaction key
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                GameObject objectHit = hit.collider.gameObject;

                if (objectHit.CompareTag("Interactable")) // You can assign a tag to your interactable objects
                {
                    // Perform interaction actions (e.g., make the object fall, display a message)
                    objectHit.GetComponent<Rigidbody>().isKinematic = false; // Disable kinematic to enable physics
                    // Add more interaction logic here

                    // Hide the interaction UI
                    interactionUI.SetActive(false);
                }
            }
        }
    }

    // Call this method to display the UI indicator with a custom message
    public void ShowInteractionUI(string message)
    {
        interactionText.text = message;
        interactionUI.SetActive(true);
    }

    // Call this method to hide the UI indicator
    public void HideInteractionUI()
    {
        interactionUI.SetActive(false);
    }
}
