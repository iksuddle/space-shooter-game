using UnityEngine;

/// <summary>
/// Bullet class represents a bullet object
/// Damages objects implementing IDamageable
/// </summary>
public class Bullet : MonoBehaviour {
    [SerializeField] private float damage = 20;
    [SerializeField] private GameObject bulletFX;

    /// <summary>
    /// Start method destroys gameobject 2 seconds after spawning
    /// </summary>
    private void Start() => Destroy(gameObject, 2f);

    /// <summary>
    /// OnTriggerEnter2D method damages IDamageable objects that this bullet collides with
    /// </summary>
    /// <param name="col">info on collision</param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Obstacle") {
            col.GetComponent<IDamageable>()?.TakeDamage(damage);

            Instantiate(bulletFX, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
