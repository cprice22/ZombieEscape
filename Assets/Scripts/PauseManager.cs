using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;   // drag your pause panel here
    public bool isPaused;

    void Start()
    {
        Resume(); // make sure game starts unpaused
    }

    public void TogglePause()
    {
        if (isPaused) Resume();
        else Pause();
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        if (pausePanel != null) pausePanel.SetActive(true);
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (pausePanel != null) pausePanel.SetActive(false);
    }
}