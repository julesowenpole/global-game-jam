using UnityEngine;
using TMPro;

public class CanvasTimer : MonoBehaviour
{
    public float timeRemaining = 60f;  // Total time in seconds (e.g., 300 for 5 minutes)
    [SerializeField] public bool timerIsRunning = false;  // Set true to auto-start
    public TextMeshProUGUI timerText;  // Assign your Canvas TextMeshProUGUI here

    private void Start()
    {
        if (timerIsRunning)
        {
            DisplayTime(timeRemaining);
        }
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Timer finished!");  // Replace with your game over logic
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;  // Ensures full seconds show during countdown (common UX)[web:2][page:1]
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
