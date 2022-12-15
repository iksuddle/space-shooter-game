using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// LoadLevelManager responsible for loading other scenes upon level completion
/// </summary>
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

    /// <summary>
    /// LoadsNextScene loads the next scene by incrementing current scene build index
    /// </summary>
    public void LoadNextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }

    /// <summary>
    /// LoadScene method loads a scene based on specific build index
    /// </summary>
    /// <param name="buildIndex">the build index of the scene to load</param>
    public void LoadScene(int buildIndex) {
        SceneManager.LoadScene(buildIndex, LoadSceneMode.Single);
    }
}
