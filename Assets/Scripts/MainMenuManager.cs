using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// MainMenuManager responsible for handling buttons in main menu
/// </summary>
public class MainMenuManager : MonoBehaviour {

    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    /// <summary>
    /// Start method assigns functions to button onclick events
    /// </summary>
    private void Start() {
        startButton.onClick.AddListener(() => LoadLevelManager.Instance.LoadNextScene());
        quitButton.onClick.AddListener(() => Application.Quit());
    }
}
