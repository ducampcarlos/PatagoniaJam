using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FunctionalityManager : MonoBehaviour
{
    public static FunctionalityManager Instance { get; private set; }

    public PlayerMovement playerMovement;
    public MouseMovement mouseMovement;
    public GameObject reticle;
    public ObjectInspection objectInspection;
    public Volume volume;

    private DepthOfField depthOfField;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Optional: if you want the manager to persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Get the Depth Of Field setting from the Volume
        if (volume.profile.TryGet<DepthOfField>(out depthOfField))
        {
            // DepthOfField component successfully retrieved
        }
    }

    public void StartInspection()
    {
        if (playerMovement != null)
            playerMovement.enabled = false;

        if (mouseMovement != null)
            mouseMovement.enabled = false;

        if (reticle != null)
            reticle.SetActive(false);

        ActivateDepthOfField(true);
    }

    public void StopInspection()
    {
        if (playerMovement != null)
            playerMovement.enabled = true;

        if (mouseMovement != null)
            mouseMovement.enabled = true;

        if (reticle != null)
            reticle.SetActive(true);

        ActivateDepthOfField(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        StopInspection();  // Ensure inspections are stopped if the game is paused

        if (objectInspection != null)
            objectInspection.StopInspectionExternally();

        ActivateDepthOfField(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        ActivateDepthOfField(false);
    }

    private void ActivateDepthOfField(bool activate)
    {
        if (depthOfField != null)
        {
            depthOfField.active = activate;
        }
    }
}
