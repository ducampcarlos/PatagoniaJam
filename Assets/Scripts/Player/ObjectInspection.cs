using UnityEngine;
using TMPro;
using UnityEngine.UI;  // For UI elements

public class ObjectInspection : MonoBehaviour
{
    public Transform playerCamera;
    public float rotationSpeed = 100f;
    public LayerMask inspectableLayer;
    public float inspectDistance = 1.5f;  // Distance in front of the camera for inspection
    public string inspectLayerName = "InspectableObject";  // Layer name for inspected objects

    public GameObject noteUI;  // Reference to the GameObject containing the background and text
    public TextMeshProUGUI noteTextDisplay;  // Reference to the UI text element for displaying note contents
    public Image reticleImage;  // Reference to the reticle image
    public Color defaultReticleColor = Color.white;
    public Color interactableReticleColor = Color.yellow;

    private bool isInspecting = false;
    private GameObject currentInspectableObject;
    private Vector3 originalObjectPosition;
    private Quaternion originalObjectRotation;
    private int originalLayer;

    private bool isNote = false;

    void Start()
    {
        // Initially hide the note UI
        if (noteUI != null)
        {
            noteUI.SetActive(false);
        }

        // Set the reticle to its default color
        if (reticleImage != null)
        {
            reticleImage.color = defaultReticleColor;
        }
    }

    void Update()
    {
        UpdateReticleColor();

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

        if (isInspecting && currentInspectableObject != null && !isNote)
        {
            RotateInspectableObject();
        }

        if (isInspecting && Input.GetKeyDown(KeyCode.Escape))
        {
            StopInspection();
        }
    }

    void UpdateReticleColor()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, 2f, inspectableLayer))
        {
            if (hit.collider.CompareTag("Inspectable") ||
                hit.collider.CompareTag("Note") ||
                hit.collider.CompareTag("Door") ||
                hit.collider.CompareTag("Drawer") ||
                hit.collider.CompareTag("Activable"))
            {
                reticleImage.color = interactableReticleColor;
            }
            else
            {
                reticleImage.color = defaultReticleColor;
            }
        }
        else
        {
            reticleImage.color = defaultReticleColor;
        }
    }

    void StartInteraction()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, 2f, inspectableLayer))
        {
            if (hit.collider.CompareTag("Inspectable"))
            {
                HandleInspectableObject(hit.collider.gameObject);
            }
            else if (hit.collider.CompareTag("Note"))
            {
                HandleNoteObject(hit.collider.gameObject);
            }
            else if (hit.collider.CompareTag("Door"))
            {
                hit.collider.GetComponent<Animator>().SetTrigger("Open");
            }
            else if (hit.collider.CompareTag("Drawer"))
            {
                hit.collider.GetComponent<Animator>().SetTrigger("Move");
            }
            else if (hit.collider.CompareTag("Activable"))
            {
                hit.collider.GetComponent<IActivable>().Activate();
            }
        }
    }

    void HandleInspectableObject(GameObject inspectableObject)
    {
        currentInspectableObject = inspectableObject;
        originalObjectPosition = currentInspectableObject.transform.position;
        originalObjectRotation = currentInspectableObject.transform.rotation;
        originalLayer = currentInspectableObject.layer;

        // Set the object to a layer that renders above others
        currentInspectableObject.layer = LayerMask.NameToLayer(inspectLayerName);

        // Disable other functionalities via the FunctionalityManager
        FunctionalityManager.Instance.StartInspection();

        // Set the object as a child of the camera but maintain its world position and rotation
        currentInspectableObject.transform.SetParent(playerCamera, true);

        // Adjust the position to be in front of the camera at the desired distance
        Vector3 adjustedPosition = playerCamera.position + playerCamera.forward * inspectDistance;
        currentInspectableObject.transform.position = adjustedPosition;

        // Maintain the object's original world rotation
        currentInspectableObject.transform.rotation = originalObjectRotation;

        isInspecting = true;
        isNote = false;  // This is not a note, so allow rotation
    }




    void HandleNoteObject(GameObject noteObject)
    {
        currentInspectableObject = noteObject;

        // Disable other functionalities via the FunctionalityManager
        FunctionalityManager.Instance.StartInspection();

        // Display the note's text on the screen
        Note noteComponent = noteObject.GetComponent<Note>();
        if (noteComponent != null && noteTextDisplay != null)
        {
            noteTextDisplay.text = noteComponent.textContent;  // Assuming Note has a textContent string field
            noteUI.SetActive(true);  // Activate the UI GameObject
        }

        // Ensure the note object is behind the UI
        Renderer noteRenderer = noteObject.GetComponent<Renderer>();
        if (noteRenderer != null)
        {
            noteRenderer.sortingOrder = -1;  // Adjust as needed
        }

        isInspecting = true;
        isNote = true;  // This is a note, so don't allow rotation
    }

    public void StopInspectionExternally()
    {
        if (isInspecting)
        {
            StopInspection();
        }
    }

    void StopInspection()
    {
        if (currentInspectableObject != null)
        {
            // Reset the object's position, rotation, and layer if it's not a note
            if (!isNote)
            {
                currentInspectableObject.transform.SetParent(null);
                currentInspectableObject.transform.position = originalObjectPosition;
                currentInspectableObject.transform.rotation = originalObjectRotation;
                currentInspectableObject.layer = originalLayer;
            }

            // Disable the note UI
            if (isNote && noteUI != null)
            {
                noteUI.SetActive(false);
            }

            // Enable other functionalities via the FunctionalityManager
            FunctionalityManager.Instance.StopInspection();

            currentInspectableObject = null;
            isInspecting = false;
            isNote = false;  // Reset the note flag
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
