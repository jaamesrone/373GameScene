using System.Collections;
using UnityEngine;

public class RCCarController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float boostSpeed = 10f;
    public float boostCooldown = 5f;
    public float rotationSpeed = 180f; // Adjust the rotation speed as needed

    private bool isBoosting = false;

    void Update()
    {
        // Check for boost input and start the boost coroutine
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isBoosting)
        {
            StartCoroutine(ActivateBoost());
        }

        // Move the RC car based on input
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(vertical, 0f, - horizontal) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Rotate the RC car based on mouse input
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.deltaTime);
    }

    IEnumerator ActivateBoost()
    {
        // Start the boost
        isBoosting = true;

        // Apply the boost speed
        moveSpeed += boostSpeed;

        // Wait for the boost duration
        yield return new WaitForSeconds(boostCooldown);

        // Reset speed and end the boost
        moveSpeed -= boostSpeed;
        isBoosting = false;
    }
}
