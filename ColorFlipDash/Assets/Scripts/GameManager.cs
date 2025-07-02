using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Global Obstacle Speed")]
    public float baseSpeed = 2f;
    public float speedIncreaseRate = 0.2f; // Per second
    public float maxSpeed = 8f;

    private float timer;

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
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
    public void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0f; // Pause the game
                             // TODO: Show game over UI
    }

}
