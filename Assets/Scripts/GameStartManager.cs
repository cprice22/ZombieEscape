using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    public GameObject startPanel;

    void Awake()
    {
        // Ensure reset every time you press Play / restart
        GameState.Started = false;
        Time.timeScale = 0f;

        if (startPanel != null)
            startPanel.SetActive(true);
    }

    public void StartGame()
    {
        GameState.Started = true;
        if (startPanel != null)
            startPanel.SetActive(false);

        Time.timeScale = 1f;

        // Optional: start timer if you added that
        FindFirstObjectByType<GameTimer>()?.Begin();
    }
}