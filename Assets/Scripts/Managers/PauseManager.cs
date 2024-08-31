using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;  // Reference to the pause menu UI

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))  // Example key to pause/unpause
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        FunctionalityManager.Instance.PauseGame();
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        FunctionalityManager.Instance.ResumeGame();
    }
}
