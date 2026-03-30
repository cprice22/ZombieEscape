using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text finishTimeText;

    public float ElapsedTime { get; private set; }
    private bool running = false;

    void Start()
    {
        ElapsedTime = 0f;
        running = false;
        UpdateHUD();

        if (finishTimeText != null)
            finishTimeText.text = "Finish Time: 00:00.00";
    }

    void Update()
    {
        if (!running) return;

        ElapsedTime += Time.deltaTime;
        UpdateHUD();
    }

    public void Begin()
    {
        running = true;
    }

    public void StopAndShowFinish()
    {
        running = false;
        UpdateHUD();

        if (finishTimeText != null)
            finishTimeText.text = $"Finish Time: {FormatTime(ElapsedTime)}";
    }

    private void UpdateHUD()
    {
        if (timerText != null)
            timerText.text = $"Time: {FormatTime(ElapsedTime)}";
    }

    private string FormatTime(float seconds)
    {
        int minutes = Mathf.FloorToInt(seconds / 60f);
        float remaining = seconds - minutes * 60f;
        return $"{minutes:00}:{remaining:00.00}";
    }
}