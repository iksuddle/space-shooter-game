using UnityEngine;
using UnityEngine.UI;

public class FinalMenuManager : MonoBehaviour {

    [SerializeField] private Button quitButton;
    [SerializeField] private Button menuButton;

    private void Start() {
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });

        menuButton.onClick.AddListener(() => {
            LoadLevelManager.Instance.LoadScene(0);
        });
    }
}
