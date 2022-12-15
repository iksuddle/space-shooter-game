using UnityEngine;

/// <summary>
/// Enemy space ship object
/// WIP
/// </summary>
public class Enemies : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction;
    public float speed;
    public float shootingDelay;
    public float lastTimeShot = 0f;
    public float bulletSpeed;
    public Transform player;
    public GameObject bullet;

    void Start() {
        player=GameObject.FindWithTag ("Player").transform;    
    }

    void Update() {
        if (Time.time > lastTimeShot + shootingDelay) {
            float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);

            GameObject newBullet = Instantiate(bullet, transform.position,q);

            newBullet.GetComponent<Rigidbody2D> ().AddRelativeForce (new Vector2(0f, bulletSpeed));
            lastTimeShot = Time.time;
        }
    }

    void FixedUpdate() {
        direction = (player.position - transform.position).normalized;
        rb.MovePosition (rb.position + direction * speed * Time.fixedDeltaTime);
    }
}
