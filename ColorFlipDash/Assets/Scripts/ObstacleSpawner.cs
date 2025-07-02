using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Settings")]
    public GameObject[] obstaclePrefabs;

    [Header("Spawn Timing")]
    public float spawnInterval = 1.5f;
    public float spawnY = 6f;

    [Header("Spawn Bounds")]
    public float leftLimit = -2.5f;
    public float rightLimit = 2.5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 1f, spawnInterval);
    }

    void SpawnObstacle()
    {
        float randomX = Random.Range(leftLimit, rightLimit);
        Vector3 spawnPos = new Vector3(randomX, spawnY, 0f);

        int randomIndex = Random.Range(0, obstaclePrefabs.Length);
        Instantiate(obstaclePrefabs[randomIndex], spawnPos, Quaternion.identity);
    }
}
