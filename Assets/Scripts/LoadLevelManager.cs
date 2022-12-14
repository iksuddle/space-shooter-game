using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelManager : MonoBehaviour {
    
    #region Singleton

    private static LoadLevelManager instance;

    public static LoadLevelManager Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<LoadLevelManager>();
            }
            return instance;
        }
    }

    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    public void LoadNextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }

    public void LoadScene(int buildIndex) {
        SceneManager.LoadScene(buildIndex, LoadSceneMode.Single);
    }
}
