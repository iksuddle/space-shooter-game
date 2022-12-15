using UnityEngine;

/// <summary>
/// PlayerMovement module responsible for moving/rotating the player based on user input
/// </summary>
public class PlayerMovement : MonoBehaviour {
    
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Camera mainCamera;

    [Header("Options")]
    [SerializeField] private float moveForce;

    private Vector2 input;
    
    private float rotation;

    /// <summary>
    /// Awake assigns main camera if null
    /// </summary>
    private void Awake() {
        if (mainCamera == null)
            mainCamera = Camera.main;
        else
            Debug.LogError("Assign Main Camera field in inspector.");
    }

    /// <summary>
    /// Update gets user input and rotation
    /// </summary>
    private void Update() {
        input = Vector2.up * Input.GetAxisRaw("Vertical") + Vector2.right * Input.GetAxisRaw("Horizontal");

        rotation = GetAngle();
    }

    /// <summary>
    /// FixedUpdate applies rigidbody forces
    /// </summary>
    private void FixedUpdate() {
        rb.AddForce(input.normalized * moveForce);
        rb.MoveRotation(rotation);
    }

    /// <summary>
    /// GetAngle method gets the angle for the spaceship needed to point at the mouse
    /// </summary>
    /// <returns>
    /// The calculated angle as a float
    /// </returns>
    private float GetAngle() {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 difference = mouseWorldPos - transform.position;
        return Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90f;
    }

    /// <summary>
    /// OnCollisionEnter2D method destroys the player upon collision with obstacle
    /// </summary>
    /// <param name="collision">info on the collision</param>
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Obstacle") {
            LevelManager.Instance.OnPlayerDeath();
            Destroy(gameObject);
        }
    }
}
