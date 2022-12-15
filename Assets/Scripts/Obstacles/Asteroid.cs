using UnityEngine;

/// <summary>
/// Asteroid represents an asteroid that players can shoot down
/// Implements IDamageable
/// </summary>
public class Asteroid : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject asteroidDestroyFX;
    [SerializeField] private float maxHealth;
    [SerializeField] private float lifeTime;

    private Rigidbody2D rb;

    private float health;
    private float timeAlive;
    
    /// <summary>
    /// Awake method assigns the rigidbody
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Start method sets initial health
    /// </summary>
    private void Start() {
        health = maxHealth;
    }

    /// <summary>
    /// Update method increments timeAlive, and checks when to destroy the asteroid
    /// </summary>
    private void Update()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive >= lifeTime)
            Destroy(gameObject);

        if (LevelManager.Instance.LevelComplete)
            Destroy(gameObject);
    }

    /// <summary>
    /// DestroyAsteroid method spawns effects and removes the asteroid game object
    /// </summary>
    private void DestroyAsteroid() 
    {
        // spawn fx
        LevelManager.Instance.OnEnemyShot();
        var go = Instantiate(asteroidDestroyFX, transform.position, Quaternion.identity);
        go.transform.localScale = transform.localScale;
        Destroy(gameObject);
    }

    /// <summary>
    /// SetTrajectory sets the direction and speed of the asteroid's velocity
    /// </summary>
    /// <param name="direction">the direction of movement</param>
    /// <param name="speed">the speed of the movement</param>
    public void SetTrajectory(Vector2 direction, float speed)
    {
        rb.AddForce(direction * speed);
    }

    /// <summary>
    /// TakeDamage damages the player, if they have no more health it destroys the asteroid
    /// </summary>
    /// <param name="damage">the amount of damage to take</param>
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f) {
            DestroyAsteroid();
        }
    }
}
