using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondPopUp : MonoBehaviour
{
    public Transform headTransform; // Reference to the mannequin head
    public Text interactionText; // Reference to the UI text
    public float upwardForce = 500f; // Adjust this value to control the upward force
    public int requiredDetachedHeads = 3; // Adjust this value to set the threshold for your specific code

    private bool isNearMannequin = false;
    private int detachedHeadCount = 0;

    void Update()
    {
        if (isNearMannequin)
        {
            interactionText.text = "Press 'E' to interact";

            if (Input.GetKeyDown(KeyCode.E))
            {
                DetachHead();
            }
        }
        else
        {
            interactionText.text = "";
        }

        // Check if the detached head count has reached the required amount
        if (detachedHeadCount >= requiredDetachedHeads)
        {
            // Trigger your specific code here
            Debug.Log("Triggering specific code!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearMannequin = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearMannequin = false;
            interactionText.text = "";
        }
    }

    private void DetachHead()
    {
        // Check if the headTransform is not null and has a Rigidbody component
        if (headTransform != null && headTransform.TryGetComponent(out Rigidbody headRigidbody))
        {
            // Detach the head from the body
            headTransform.parent = null;

            // Apply an upward force to make the head pop up
            headRigidbody.AddForce(Vector3.up * upwardForce);

            // Increment the detached head count
            detachedHeadCount++;

            // Your additional code goes here...
        }
    }
}
