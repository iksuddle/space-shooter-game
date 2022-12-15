using UnityEngine;

/// <summary>
/// Asteroid Spawner is responsible for spawning asteroids randomly and directing them at the player
/// </summary>
public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;
    [Space]
    [SerializeField] private float spawnDistance;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float numberPerSpawn;
    [Space]
    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;
    [Space]
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    /// <summary>
    /// Start method invokes the SpawnAsteroids method based on configured interval
    /// </summary>
    private void Start()
    {
        InvokeRepeating(nameof(SpawnAsteroids), spawnInterval, spawnInterval);
    }

    /// <summary>
    /// SpawnAsteroids spawns asteroids randomly based on the configured fields
    /// </summary>
    private void SpawnAsteroids()
    {
        if (LevelManager.Instance.LevelComplete) return;
        if (LevelManager.Instance.ResettingLevel) return;

        for (int i = 0; i < numberPerSpawn; i++)
        {
            Vector2 spawnDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            Vector2 spawnPoint = spawnDirection * spawnDistance;

            Quaternion spawnRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

            GameObject asteroid = Instantiate(asteroidPrefab, spawnPoint, spawnRotation);
            float asteroidRadius = Random.Range(minScale, maxScale);
            asteroid.transform.localScale = new Vector3(asteroidRadius, asteroidRadius, 1f);

            Vector2 asteroidTrajectory = -spawnDirection.normalized;
            asteroid.GetComponent<Asteroid>().SetTrajectory(asteroidTrajectory, Random.Range(minSpeed, maxSpeed));
        }
    }
}
