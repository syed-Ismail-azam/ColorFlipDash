using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Prefabs")]
    public GameObject[] obstaclePrefabs; // Drag all 4 prefabs into this array in Inspector

    [Header("Spawn Settings")]
    public float spawnInterval = 1.5f;
    public float spawnY = 6f;
    public float leftLimit = -2.5f;
    public float rightLimit = 2.5f;
    public float moveSpeed = 2f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 1f, spawnInterval);
    }

    void SpawnObstacle()
    {
        // Random X position
        float randomX = Random.Range(leftLimit, rightLimit);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);

        // Random prefab
        int randomIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject chosenObstacle = obstaclePrefabs[randomIndex];

        // Instantiate & move
        GameObject obstacle = Instantiate(chosenObstacle, spawnPosition, Quaternion.identity);
        ObstacleMover mover = obstacle.AddComponent<ObstacleMover>();
        mover.moveSpeed = moveSpeed;
    }
}
