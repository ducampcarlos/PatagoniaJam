using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    float xRotation = 0f;
    float YRotation = 0f;

    [SerializeField] Transform cam;

    void Start()
    {
        // Locking the cursor to the middle of the screen and making it invisible
        Cursor.lockState = CursorLockMode.Locked;

        // Initialize rotation values based on current rotation
        InitializeRotation();
    }

    private void OnEnable()
    {
        // Update rotation values based on the current rotation when the script is enabled
        InitializeRotation();
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Control rotation around the x-axis (Look up and down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Control rotation around the y-axis (Look left and right)
        YRotation += mouseX;

        // Apply both rotations
        transform.localRotation = Quaternion.Euler(0, YRotation, 0f);
        cam.localRotation = Quaternion.Euler(xRotation, 0, 0f);
    }

    void InitializeRotation()
    {
        YRotation = transform.localRotation.eulerAngles.y;
        xRotation = cam.localRotation.eulerAngles.x;

        // Correct the xRotation if it exceeds 180 degrees
        if (xRotation > 180)
        {
            xRotation -= 360; // Convert 360-0 range to -180 to 180 range
        }
    }
}
