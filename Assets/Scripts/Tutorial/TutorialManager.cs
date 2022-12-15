using UnityEngine;

/// <summary>
/// Tutorial Manager is responsible for managing the state of the tutorial level
/// </summary>
public class TutorialManager : MonoBehaviour
{
    private int stage;

    [SerializeField] private GameObject[] texts;
    [SerializeField] private Transform playerTransform;

    private bool loadingNextLevel;

    // movement stage
    private Vector2 pressed = Vector2.zero;

    // looking stage
    private bool startedLookStage;
    private Vector2 initialDirection;

    // shooting stage
    private float timeHeld;

    /// <summary>
    /// Start method sets initial stage to 1
    /// </summary>
    private void Start() {
        ChangeStage(1);
    }

    /// <summary>
    /// Update method handles high level state management
    /// </summary>
    private void Update() {
        switch (stage) {
            case 1:
                MovementStage();
                break;
            case 2:
                LookingStage();
                break;
            case 3:
                ShootingStage();
                break;
            case 4:
                Complete();
                break;
        }
    }

    /// <summary>
    /// MovementStage method handles the movement stage
    /// </summary>
    private void MovementStage() {
        if (Input.GetAxisRaw("Horizontal") != 0f)
            pressed.x = 1;
        if (Input.GetAxisRaw("Vertical") != 0f)
            pressed.y = 1;
        
        if (pressed.x == 1 && pressed.y == 1) {
            ChangeStage(2);
        }
    }

    /// <summary>
    /// LookingStage method handles the looking stage
    /// </summary>
    private void LookingStage() {
        if (!startedLookStage) {
            initialDirection = playerTransform.up;
            startedLookStage = true;
        } else {
            if (Vector2.Dot(playerTransform.up, initialDirection) <= -0.6f)
                ChangeStage(3);
        }
    }

    /// <summary>
    /// ShootingStage method handles the shooting stage
    /// </summary>
    private void ShootingStage() {
        if (Input.GetKey(KeyCode.Mouse0))
            timeHeld += Time.deltaTime;
        if (timeHeld >= 2f)
            ChangeStage(4);
    }

    /// <summary>
    /// Complete method loads the next level upon completion
    /// </summary>
    private void Complete() {
        if (!loadingNextLevel) {
            Invoke(nameof(LoadNextLevel), 3f);
            loadingNextLevel = true;
        }
    }

    /// <summary>
    /// LoadNextLevel method is a wrapper method to load the next level (can't invoke otherwise)
    /// </summary>
    private void LoadNextLevel() {
        LoadLevelManager.Instance.LoadNextScene();
    }

    /// <summary>
    /// ChangeState method responsible for changing the tutorial stage
    /// </summary>
    /// <param name="newStage">int for the next stage</param>
    private void ChangeStage(int newStage) {
        stage = newStage;

        foreach (var text in texts)
            text.SetActive(false);

        texts[stage-1].SetActive(true);
    }
}
