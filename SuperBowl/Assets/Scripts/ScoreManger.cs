using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; } // Singleton instance

    private int totalScore = 0;

    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI scoreText; // Reference to TextMeshPro UI element

    private void Awake()
    {
        // Ensure only one instance of ScoreManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes if needed
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreUI(); // Initialize the UI
    }

    public void AddPoints(int points)
    {
        totalScore += points;
        Debug.Log($"Points added: {points}. Total Score: {totalScore}");
        UpdateScoreUI();
    }

    public int GetTotalScore()
    {
        return totalScore;
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {totalScore}";
        }
        else
        {
            Debug.LogWarning("Score Text UI is not assigned in ScoreManager.");
        }
    }
}

