using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.LowLevel;

public class LevelManager : MonoBehaviour {

    [SerializeField] private TMP_Text scoreUI;
    [SerializeField] private GameObject playerPrefab;
    [Space]
    [SerializeField] private int scoreToBeat;
    [SerializeField] private float resetDelay;

    public bool ResettingLevel { get; private set; }
    public bool LevelComplete { get; private set; }
    public int CurrentScore { get; private set; }

    private bool loadingNextLevel;

    #region Singleton
    
    private static LevelManager instance;

    public static LevelManager Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<LevelManager>();
            }
            return instance;
        }
    }

    private void Awake() {
        if (instance != null) Destroy(gameObject);
    }

    #endregion

    private void Start() {
        LevelComplete = false;
        UpdateScoreUI(0);
    }

    public void OnPlayerDeath() {
        StartCoroutine(nameof(ResetLevel));
    }

    IEnumerator ResetLevel() {
        ResettingLevel = true;
        // destroy all asteroids
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();
        for (int i = 0; i < asteroids.Length; i++)
            Destroy(asteroids[i].gameObject);
        // wait 
        yield return new WaitForSeconds(resetDelay);
        // reset score
        CurrentScore = 0;
        UpdateScoreUI(CurrentScore);
        // respawn player
        Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        ResettingLevel = false;
    }

    public void OnEnemyShot() {
        CurrentScore++;
        UpdateScoreUI(CurrentScore);
        if (CurrentScore >= scoreToBeat)
            OnLevelComplete();
    }

    private void UpdateScoreUI(int newScore) {
        scoreUI.text = $"{scoreToBeat - newScore}";
    }

    private void OnLevelComplete() {
        LevelComplete = true;
        if (!loadingNextLevel) {
            loadingNextLevel = true;
            Invoke(nameof(LoadNextLevel), 2f);
        }
    }

    private void LoadNextLevel() {
        LoadLevelManager.Instance.LoadNextScene();
    }
}
