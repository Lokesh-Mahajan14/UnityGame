//using UnityEngine;
//using UnityEngine.UI;

//public class GameManagerPath : MonoBehaviour
//{
//    private static GameManagerPath _instance;
//    public static GameManagerPath Instance => _instance ??= FindObjectOfType<GameManagerPath>();

//    [SerializeField] private Text _scoreText;  // Serialized field
//    [SerializeField] private GameObject _gameOverPanel;
//    private int _score = 0;
//    private bool _isGameActive = true;

//    [Header("Start Menu")]
//    [SerializeField] private GameObject startMenu;

//    // Public properties
//    public int Score
//    {
//        get => _score;
//        set
//        {
//            _score = value;
//            UpdateScoreDisplay();
//        }
//    }

//    public Text ScoreText => _scoreText;  // Read-only access
//    public GameObject GameOverPanel => _gameOverPanel;

//    public bool IsGameActive
//    {
//        get => _isGameActive;
//        set => _isGameActive = value;
//    }

//    void Awake() => InitializeSingleton();

//    private void InitializeSingleton()
//    {
//        if (_instance == null)
//        {
//            _instance = this;
//            DontDestroyOnLoad(gameObject);
//        }
//        else if (_instance != this)
//        {
//            Destroy(gameObject);
//        }
//    }

//    public void CaughtFruit() => Score += 10;
//    public void MissedFruit() { /* Optional penalty */ }
//    public void HitBomb() => GameOver();

//    private void UpdateScoreDisplay()
//    {
//        if (_scoreText != null)
//            _scoreText.text = $"Score: {Score}";
//    }

//    public void GameOver()
//    {
//        IsGameActive = false;
//        _gameOverPanel?.SetActive(true);
//    }

//    public void RestartGame()
//    {
//        Score = 0;
//        IsGameActive = true;
//        _gameOverPanel?.SetActive(false);

//        foreach (var obj in FindObjectsOfType<MovingObjectPath>())
//            Destroy(obj.gameObject);
//    }

//    public void StartGame()
//{
//    IsGameActive = true;
//    startMenu.SetActive(false); // Hide menu
//    FindObjectOfType<SpawnManagerPath>().StartSpawning(); // Start spawning
//}

//    public void ShowStartMenu()
//    {
//        IsGameActive = false;
//        startMenu.SetActive(true);
//    }
//}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerPath : MonoBehaviour
{
    // Singleton instance
    private static GameManagerPath _instance;
    public static GameManagerPath Instance => _instance ??= FindObjectOfType<GameManagerPath>();

    [Header("UI References")]
    [SerializeField] private TMP_Text scoreText;  // Changed to lowercase for Unity convention
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject startMenu;

    private int score = 0;
    private bool isGameActive = true;

    // Public properties
    public int Score
    {
        get => score;
        set
        {
            score = value;
            UpdateScoreDisplay();
        }
    }

    public bool IsGameActive
    {
        get => isGameActive;
        set => isGameActive = value;
    }

    void Awake() => InitializeSingleton();

    private void InitializeSingleton()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void CaughtFruit() => Score += 10;
    public void MissedFruit() { /* Optional penalty */ }
    public void HitBomb() => GameOver();

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {Score}";
        else
            Debug.LogError("ScoreText is not assigned!");
    }

    public void GameOver()
    {
        IsGameActive = false;
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Score = 0;
        IsGameActive = true;
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        foreach (var obj in FindObjectsOfType<MovingObjectPath>())
            Destroy(obj.gameObject);
    }

    //public void StartGame()
    //{
    //    IsGameActive = true;
    //    if (startMenu != null)
    //        startMenu.SetActive(false);
    //    FindObjectOfType<SpawnManagerPath>()?.StartSpawning();
    //}

    public void StartGame()
    {
        IsGameActive = true;
        if (startMenu != null)
            startMenu.SetActive(false);

        // Start spawning immediately
        SpawnManagerPath spawnManager = FindObjectOfType<SpawnManagerPath>();
        if (spawnManager != null)
        {
            spawnManager.StartSpawning();
        }
        else
        {
            Debug.LogError("SpawnManager not found!");
        }
    }

    public void ShowStartMenu()
    {
        IsGameActive = false;
        if (startMenu != null)
            startMenu.SetActive(true);
    }
}