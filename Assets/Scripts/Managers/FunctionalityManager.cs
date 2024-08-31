using UnityEngine;

public class FunctionalityManager : MonoBehaviour
{
    public static FunctionalityManager Instance { get; private set; }

    public PlayerMovement playerMovement;
    public MouseMovement mouseMovement;
    public GameObject reticle;
    public ObjectInspection objectInspection;

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

    public void StartInspection()
    {
        if (playerMovement != null)
            playerMovement.enabled = false;

        if (mouseMovement != null)
            mouseMovement.enabled = false;

        if (reticle != null)
            reticle.SetActive(false);
    }

    public void StopInspection()
    {
        if (playerMovement != null)
            playerMovement.enabled = true;

        if (mouseMovement != null)
            mouseMovement.enabled = true;

        if (reticle != null)
            reticle.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        StopInspection();  // Ensure inspections are stopped if the game is paused

        if (objectInspection != null)
            objectInspection.StopInspectionExternally();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
