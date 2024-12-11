using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [Header("Timer Settings")]
    public float startTime = 120f; // 2 minutes in seconds
    private float currentTime;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI timerText; // Reference to Timer UI
    [SerializeField] private TextMeshProUGUI endGameText; // Reference to End Game UI

    private bool isGameRunning = true;

    void Start()
    {
        currentTime = startTime;
        UpdateTimerUI();
        endGameText.gameObject.SetActive(false); // Hide end game text at start
    }

    void Update()
    {
        if (isGameRunning)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;
                EndGame();
            }

            UpdateTimerUI();
        }
    }

    private void UpdateTimerUI()
    {
        // Format time as MM:SS
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = $"Time: {minutes:00}:{seconds:00}";
    }

    private void EndGame()
    {
        isGameRunning = false;
        timerText.text = "Time: 00:00";
        
        // Show end game message with total points
        int finalScore = ScoreManager.Instance.GetTotalScore();
        endGameText.text = $"Game Over\nFinal Score: {finalScore}";
        endGameText.gameObject.SetActive(true);

        // Add additional game-ending logic here (e.g., stop enemy spawns)
        Time.timeScale = 0; // Pause the game

          // Delay before returning to main menu
        Invoke(nameof(QuitGame), 3f); // Wait 3 seconds before returning
    }

    public void QuitGame()
    {
        Application.Quit();
    }

   
}