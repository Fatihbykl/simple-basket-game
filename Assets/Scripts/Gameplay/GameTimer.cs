using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public int duration;
    public static GameTimer Instance;
    public TextMeshProUGUI timerText;
    private float timeRemaining;
    private bool isTimerRunning;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void StartTimer()
    {
        timeRemaining = duration;
        isTimerRunning = true;
    }

    public void ResetTimer()
    {
        timeRemaining = duration;
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Ceil(timeRemaining);

            if (timeRemaining <= 0)
            {
                isTimerRunning = false;
                GameOver();
            }
        }
    }

    void GameOver()
    {
        Debug.Log("OYUN BİTTİ! Süre doldu.");
    }
}
