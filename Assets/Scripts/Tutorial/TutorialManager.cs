using UnityEngine;

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

    private void Start() {
        ChangeStage(1);
    }

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

    private void MovementStage() {
        if (Input.GetAxisRaw("Horizontal") != 0f)
            pressed.x = 1;
        if (Input.GetAxisRaw("Vertical") != 0f)
            pressed.y = 1;
        
        if (pressed.x == 1 && pressed.y == 1) {
            ChangeStage(2);
        }
    }
    
    private void LookingStage() {
        if (!startedLookStage) {
            initialDirection = playerTransform.up;
            startedLookStage = true;
        } else {
            if (Vector2.Dot(playerTransform.up, initialDirection) <= -0.6f)
                ChangeStage(3);
        }
    }

    private void ShootingStage() {
        if (Input.GetKey(KeyCode.Mouse0))
            timeHeld += Time.deltaTime;
        if (timeHeld >= 2f)
            ChangeStage(4);
    }

    private void Complete() {
        if (!loadingNextLevel) {
            Invoke(nameof(LoadNextLevel), 3f);
            loadingNextLevel = true;
        }
    }

    private void LoadNextLevel() {
        LoadLevelManager.Instance.LoadNextScene();
    }

    private void ChangeStage(int newStage) {
        stage = newStage;

        foreach (var text in texts)
            text.SetActive(false);

        texts[stage-1].SetActive(true);
    }
}
