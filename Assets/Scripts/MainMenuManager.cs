using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

    [SerializeField] private Button startButton;

    private void Start() {
        startButton.onClick.AddListener(() => LoadLevelManager.Instance.LoadNextScene());
    }
}
