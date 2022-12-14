using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    private void Start() {
        startButton.onClick.AddListener(() => LoadLevelManager.Instance.LoadNextScene());
        quitButton.onClick.AddListener(() => Application.Quit());
    }
}
