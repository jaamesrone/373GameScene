using System.Collections;
using UnityEngine;

public class RCCarController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float boostSpeed = 10f;
    public float boostCooldown = 5f;
    public float rotationSpeed = 180f;

    private bool isBoosting = false;
    private Transform cameraTransform;

    void Start()
    {
        FreeRoam();
    }

    void Update()
    {
        // Check for boost input and start the boost coroutine
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isBoosting)
        {
            StartCoroutine(ActivateBoost());
        }

        rcCarMovement();

        mouseLook();
    }

    IEnumerator ActivateBoost()
    {
        // Store the initial local position of the camera
        Vector3 initialLocalPosition = cameraTransform.localPosition;

        // Start the boost
        isBoosting = true;

        // Move the camera along its local x-axis by -1 unit
        cameraTransform.localPosition += new Vector3(-1, 0, 0);

        // Apply the boost speed
        moveSpeed += boostSpeed;

        // Wait for the boost duration
        yield return new WaitForSeconds(boostCooldown);

        // Reset the camera's local position to the initial local position
        cameraTransform.localPosition = initialLocalPosition;

        // Reset speed and end the boost
        moveSpeed -= boostSpeed;

        isBoosting = false;
    }



    private void FreeRoam()
    {
        // attatching the camera that is in prefab of the RC car.
        Camera rcCarCamera = GetComponentInChildren<Camera>();
        if (rcCarCamera != null)
        {
            cameraTransform = rcCarCamera.transform;
        }
    }

    private void rcCarMovement()
    {
        // Move the RC car based on input
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(vertical, 0f, -horizontal) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    private void mouseLook()
    {
        // Rotate the RC car based on mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.deltaTime);

        // Rotate the camera (or the assigned cameraTransform) based on mouse input
        if (cameraTransform != null)
        {
            cameraTransform.Rotate(Vector3.left, mouseY * rotationSpeed * Time.deltaTime);
        }
    }
}
