using UnityEngine;

/// <summary>
/// Player2Movement WIP
/// </summary>
public class Player2Movement : MonoBehaviour {
    
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
        rb = GetComponent<Rigidbody2D>();
         if (Input.GetKey(KeyCode.A))
             rb.AddForce(Vector3.left);
         if (Input.GetKey(KeyCode.D))
             rb.AddForce(Vector3.right);
         if (Input.GetKey(KeyCode.W))
             rb.AddForce(Vector3.up);
         if (Input.GetKey(KeyCode.S))
             rb.AddForce(Vector3.down);
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
