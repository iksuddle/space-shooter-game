using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    [Header("References")]
    [SerializeField] private GameObject playerPrefab;
    [Header("UI Elements")]
    [SerializeField] private TMP_Text enemiesLeftText;
    [SerializeField] private TMP_Text currentScoreText;
    [SerializeField] private TMP_Text highestScoreText;
    [SerializeField] private Image timeSinceLastKillWindow;
    [Header("Settings")]
    [SerializeField] private int enemiesLeft;
    [SerializeField] private float resetDelay;
    [SerializeField] private float timeForScoreBoost;
    [SerializeField] private string highScoreSaveKey;


    public bool ResettingLevel { get; private set; }
    public bool LevelComplete { get; private set; }
    public int EnemiesDestroyed { get; private set; }

    private float timeSinceLastEnemyKilled;
    
    private int score;
    private int highestScore;

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
        highestScore = PlayerPrefs.GetInt(highScoreSaveKey);
        highestScoreText.text = $"Highest: {highestScore}";
    }

    private void Update() {
        timeSinceLastEnemyKilled += Time.deltaTime;
        if (!LevelComplete || ResettingLevel) {
            var targetFillAmount = 1 - (timeSinceLastEnemyKilled / timeForScoreBoost);
            timeSinceLastKillWindow.fillAmount = Mathf.Lerp(timeSinceLastKillWindow.fillAmount, targetFillAmount, Time.deltaTime * 10f);
        }
    }

    public void OnPlayerDeath() {
        StartCoroutine(nameof(ResetLevel));
        SetHighScore();
        score = 0;
    }

    private void SetHighScore() {
        if (score >= highestScore) {
            PlayerPrefs.SetInt(highScoreSaveKey, score);
            highestScore = score;
        }

        highestScoreText.text = $"Highest: {highestScore}";
    }

    private IEnumerator ResetLevel() {
        ResettingLevel = true;
        // destroy all asteroids
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();
        for (int i = 0; i < asteroids.Length; i++)
            Destroy(asteroids[i].gameObject);
        // wait 
        yield return new WaitForSeconds(resetDelay);
        // reset score
        EnemiesDestroyed = 0;
        UpdateScoreUI(EnemiesDestroyed);
        // respawn player
        Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        ResettingLevel = false;
    }

    public void OnEnemyShot() {
        if (timeSinceLastEnemyKilled <= timeForScoreBoost)
            score *= 2;
        score += 30;
        timeSinceLastEnemyKilled = 0f;
        EnemiesDestroyed++;
        UpdateScoreUI(EnemiesDestroyed);
        if (EnemiesDestroyed >= enemiesLeft)
            OnLevelComplete();
    }

    private void OnLevelComplete() {
        LevelComplete = true;
        SetHighScore();
        if (!loadingNextLevel) {
            loadingNextLevel = true;
            Invoke(nameof(LoadNextLevel), 2f);
        }
    }

    private void LoadNextLevel() {
        LoadLevelManager.Instance.LoadNextScene();
    }

    private void UpdateScoreUI(int newScore) {
        enemiesLeftText.text = $"{enemiesLeft - newScore}";
        currentScoreText.text = $"Score: {score}";
    }
}
