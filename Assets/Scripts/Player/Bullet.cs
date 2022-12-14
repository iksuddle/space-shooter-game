using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] private float damage = 20;
    [SerializeField] private GameObject bulletFX;

    private void Start() => Destroy(gameObject, 2f);

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Obstacle") {
            col.GetComponent<IDamageable>()?.TakeDamage(damage);

            Instantiate(bulletFX, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
