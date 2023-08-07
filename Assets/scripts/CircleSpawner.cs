using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    public GameObject circlePrefab;
    public Transform[] spawnPoints; // Array of empty Transforms for spawn positions
    public int numberOfCircles = 10; // Number of circles to spawn
    public float spawnInterval = 2.0f; // Time interval between spawns in seconds
    public float circleLifetime = 5.0f; // Lifetime of spawned circles in seconds

    private void Start()
    {
        // Start spawning circles with the specified interval
        InvokeRepeating("SpawnCircles", 0.0f, spawnInterval);
    }

    private void SpawnCircles()
    {
        for (int i = 0; i < numberOfCircles; i++)
        {
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; // Get a random spawn point
            GameObject newCircle = Instantiate(circlePrefab, randomSpawnPoint.position, Quaternion.identity);
            CircleCollider2D circleCollider = newCircle.GetComponent<CircleCollider2D>();
            if (circleCollider != null)
            {
                circleCollider.isTrigger = true; // Set the circle collider as a trigger for collision detection
            }

            // Destroy the spawned circle after the specified lifetime
            Destroy(newCircle, circleLifetime);
        }
    }
}
