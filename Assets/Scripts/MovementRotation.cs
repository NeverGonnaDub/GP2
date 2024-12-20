using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRotation : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed
    public float cameraSensitivity = 100f; // Sensitivity
    public Transform cameraTransform; 

    private Rigidbody rb;
    private float pitch = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        // Lock in
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Handle mouse movement for camera rotation
        float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;

        // Rotate player horizontally
        transform.Rotate(0f, mouseX, 0f);

        // Rotate camera vertically (clamped)
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    void FixedUpdate()
    {
        // WASD
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W)) moveZ += 1f; // Forward
        if (Input.GetKey(KeyCode.S)) moveZ -= 1f; // Backward
        if (Input.GetKey(KeyCode.A)) moveX -= 1f; // Left
        if (Input.GetKey(KeyCode.D)) moveX += 1f; // Right


        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 movement = (forward * moveZ + right * moveX).normalized * moveSpeed;

        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }
}

