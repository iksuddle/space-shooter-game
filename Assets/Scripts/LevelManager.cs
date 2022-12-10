using System;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour {

    [SerializeField] private TMP_Text scoreUI;
    [Space]
    [SerializeField] private int scoreToBeat;

    public bool LevelComplete { get; private set; }
    public int CurrentScore { get; private set; }

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
        // start next level sequence
        print("Level Complete!");
    }
}
