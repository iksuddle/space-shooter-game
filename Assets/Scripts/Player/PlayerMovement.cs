using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Camera mainCamera;

    [Header("Options")]
    [SerializeField] private float moveForce;

    private Vector2 input;
    
    private float rotation;

    private void Awake() {
        if (mainCamera == null)
            mainCamera = Camera.main;
        else
            Debug.LogError("Assign Main Camera field in inspector.");
    }

    private void Update() {
        input = Vector2.up * Input.GetAxisRaw("Vertical") + Vector2.right * Input.GetAxisRaw("Horizontal");

        rotation = GetAngle();
    }

    private void FixedUpdate() {
        rb.AddForce(input.normalized * moveForce);
        rb.MoveRotation(rotation);
    }

    private float GetAngle() {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 difference = mouseWorldPos - transform.position;
        return Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90f;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Obstacle") {
            LevelManager.Instance.OnPlayerDeath();
            Destroy(gameObject);
        }
    }
}
