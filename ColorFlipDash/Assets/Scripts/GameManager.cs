using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TMP text support
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public ColorFlipAndMove2D playerScript;

    [Header("Effects")]
    public GameObject hitEffectPrefab;

    [Header("Obstacle Speed Settings")]
    public float baseSpeed = 2f;
    public float speedIncreaseRate = 0.2f;
    public float maxSpeed = 8f;

    [Header("UI")]
    public Text scoreText;
    public Text highScoreText;
    public Text startText; // <-- TMP "READY... GO!" text

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public Text gameOverScoreText;

    [Header("Pause UI")]
    public GameObject pausePanel;
    public GameObject pauseButton; // Optional: hide button when paused



    [Header("Audio")]
    public AudioClip readySound;
    public AudioClip goSound;

    private AudioSource audioSource;

    private float timer;
    private int score = 0;
    private int highScore = 0;
    private bool gameStarted = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        UpdateScoreUI();
        StartCoroutine(ReadyGoSequence());
    }

    void Update()
    {
        if (gameStarted)
        {
            timer += Time.deltaTime;
        }
    }

    public float GetCurrentObstacleSpeed()
    {
        if (!gameStarted) return 0f; // Don't spawn until game starts
        float currentSpeed = baseSpeed + (timer * speedIncreaseRate);
        return Mathf.Min(currentSpeed, maxSpeed);
    }

    public void AddScore(int amount)
    {
        score += amount;

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

    /*public void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0f;
        ShowGameOverUI();
    

    }*/

    public void GameOver()
    {
        Time.timeScale = 0f;
        ShowGameOverUI();
        pauseButton.SetActive(false);
        playerScript.SetInputEnabled(false);

    }
    private void ShowGameOverUI()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            if (gameOverScoreText != null)
            {
                gameOverScoreText.text = "Score: " + score + "\nHigh Score: " + highScore;
            }
        }
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameePlay");
    }
    public void MainMenueGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenue");
    }

    /*   public void PauseGame()
       {
           Time.timeScale = 0f;
           if (pausePanel != null) pausePanel.SetActive(true);
           if (pauseButton != null) pauseButton.SetActive(false);
       }*/
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
        playerScript.SetInputEnabled(false);
    }

    /* public void ResumeGame()
     {
         Time.timeScale = 1f;
         if (pausePanel != null) pausePanel.SetActive(false);
         if (pauseButton != null) pauseButton.SetActive(true);
     }*/
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
        playerScript.SetInputEnabled(true);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }


    private IEnumerator ReadyGoSequence()
    {
        Time.timeScale = 0f;

        if (startText != null)
        {
            startText.gameObject.SetActive(true);
            startText.text = "READY...";
        }

        if (readySound != null) audioSource.PlayOneShot(readySound);
        yield return new WaitForSecondsRealtime(1.5f);

        if (startText != null)
        {
            startText.text = "GO!";
        }

        if (goSound != null) audioSource.PlayOneShot(goSound);
        yield return new WaitForSecondsRealtime(2f);

        if (startText != null)
            startText.gameObject.SetActive(false);

        Time.timeScale = 1f;
        gameStarted = true;
    }
}
