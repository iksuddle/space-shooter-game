using UnityEngine;

public class Asteroid : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float lifeTime;

    private Rigidbody2D rb;

    private float health;
    private float timeAlive;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        health = maxHealth;
    }

    private void Update()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive >= lifeTime)
            Destroy(gameObject);

        if (LevelManager.Instance.LevelComplete)
            Destroy(gameObject);
    }

    private void DestroyAsteroid() 
    {
        // spawn fx
        LevelManager.Instance.OnEnemyShot();
        Destroy(gameObject);
    }

    public void SetTrajectory(Vector2 direction, float speed)
    {
        rb.AddForce(direction * speed);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f) {
            DestroyAsteroid();
        }
    }
}
