using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] private float damage = 20;

    private void Start() => Destroy(gameObject, 2f);

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Obstacle") {
            col.GetComponent<IDamageable>()?.TakeDamage(damage);
            Destroy(gameObject, 0.01f);
        }
    }
}
