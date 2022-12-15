using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// FinalMenuManager responsible for handling buttons in the final menu
/// </summary>
public class FinalMenuManager : MonoBehaviour {

    [SerializeField] private Button quitButton;
    [SerializeField] private Button menuButton;

    /// <summary>
    /// Start method assigns functions to button onClick events
    /// </summary>
    private void Start() {
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });

        menuButton.onClick.AddListener(() => {
            LoadLevelManager.Instance.LoadScene(0);
        });
    }
}
