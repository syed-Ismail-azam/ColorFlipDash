using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Obstacle Speed Settings")]
    public float baseSpeed = 2f;
    public float speedIncreaseRate = 0.2f;
    public float maxSpeed = 8f;

    [Header("UI")]
    public Text scoreText;
    public Text highScoreText; //  Add this

    private float timer;
    private int score = 0;
    private int highScore = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Load saved high score
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Start()
    {
        UpdateScoreUI();
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    public float GetCurrentObstacleSpeed()
    {
        float currentSpeed = baseSpeed + (timer * speedIncreaseRate);
        return Mathf.Min(currentSpeed, maxSpeed);
    }

    public void AddScore(int amount)
    {
        score += amount;

        // Update high score if needed
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();

        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore.ToString();
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0f;
    }
}
