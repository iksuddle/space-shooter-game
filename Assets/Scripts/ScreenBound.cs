using UnityEngine;

public class ScreenBound : MonoBehaviour {
    
    [Header("Screen Bound Object Settings")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float buffer;

    private float leftBorder;
    private float rightBorder;
    private float topBorder;
    private float bottomBorder;

    private void Awake() {
        if (mainCamera == null)
            mainCamera = Camera.main;
        else
            Debug.LogError("Assign Main Camera field in inspector.");
    }

    private void Start() {
        topBorder = mainCamera.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y;
        rightBorder = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x;
        bottomBorder = mainCamera.ScreenToWorldPoint(Vector3.zero).y;
        leftBorder = mainCamera.ScreenToWorldPoint(Vector3.zero).x;
    }

    private void Update() {
        // left border
        if (transform.position.x < leftBorder - buffer)
            transform.position = new Vector3(rightBorder, transform.position.y, transform.position.z);
        // right border
        if (transform.position.x > rightBorder + buffer)
            transform.position = new Vector3(leftBorder, transform.position.y, transform.position.z);
        // bottom border
        if (transform.position.y < bottomBorder - buffer)
            transform.position = new Vector3(transform.position.x, topBorder, transform.position.z);
        // top border
        if (transform.position.y > topBorder + buffer)
            transform.position = new Vector3(transform.position.x, bottomBorder, transform.position.z);
    }
}
