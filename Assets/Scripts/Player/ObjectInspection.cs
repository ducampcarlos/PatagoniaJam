using UnityEngine;

public class ObjectInspection : MonoBehaviour
{
    public Transform playerCamera;
    public float rotationSpeed = 100f;
    public LayerMask inspectableLayer;
    public float inspectDistance = 1.5f;  // Distance in front of the camera for inspection
    public string inspectLayerName = "InspectableObject";  // Layer name for inspected objects

    private bool isInspecting = false;
    private GameObject currentInspectableObject;
    private Vector3 originalObjectPosition;
    private Quaternion originalObjectRotation;
    private int originalLayer;
    private PlayerMovement playerMovement;
    private MouseMovement mouseMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        mouseMovement = GetComponent<MouseMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isInspecting)
            {
                StopInspection();
            }
            else
            {
                StartInteraction();
            }
        }

        if (isInspecting && currentInspectableObject != null)
        {
            RotateInspectableObject();
        }
    }

    void StartInteraction()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, 5f, inspectableLayer))
        {
            if (hit.collider.CompareTag("Inspectable"))
            {
                currentInspectableObject = hit.collider.gameObject;
                originalObjectPosition = currentInspectableObject.transform.position;
                originalObjectRotation = currentInspectableObject.transform.rotation;
                originalLayer = currentInspectableObject.layer;

                // Set the object to a layer that renders above others (adjust your layer setup accordingly)
                currentInspectableObject.layer = LayerMask.NameToLayer(inspectLayerName);

                // Disable player movement
                playerMovement.enabled = false;
                mouseMovement.enabled = false;

                // Bring the object in front of the camera
                currentInspectableObject.transform.SetParent(playerCamera);
                currentInspectableObject.transform.localPosition = new Vector3(0, 0, inspectDistance);
                currentInspectableObject.transform.localRotation = Quaternion.identity;

                isInspecting = true;
            }
            else if (hit.collider.CompareTag("Door"))
            {
                //Open door
            }
            else if (hit.collider.CompareTag("Note"))
            {
                //See Note
            }
        }
    }

    void StopInspection()
    {
        if (currentInspectableObject != null)
        {
            // Reset the object's position, rotation, and layer
            currentInspectableObject.transform.SetParent(null);
            currentInspectableObject.transform.position = originalObjectPosition;
            currentInspectableObject.transform.rotation = originalObjectRotation;
            currentInspectableObject.layer = originalLayer;

            // Enable player movement
            playerMovement.enabled = true;
            mouseMovement.enabled = true;

            currentInspectableObject = null;
            isInspecting = false;
        }
    }

    void RotateInspectableObject()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        currentInspectableObject.transform.Rotate(Vector3.up, -mouseX, Space.World);
        currentInspectableObject.transform.Rotate(Vector3.right, mouseY, Space.World);
    }
}
