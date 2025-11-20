using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreLabel;
    [SerializeField] private TMP_Text healthLabel;
    [SerializeField] private TMP_Text gameOverScoreLabel;
    [SerializeField] private GameObject inGameMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private PlayerProfile playerProfile;

    [Header("Dialogue")]
    [SerializeField] private string dialogueContent;
    [SerializeField] private GameObject dialogueGameObject;
    [SerializeField] private TMP_Text dialogueLabel;
    [SerializeField] private float dialogueDuration;

    public static GameController Instance;
    public static event Action Replay;
    public static event Action BossEnemyAppear;
    public static event Action BossEnemyFightStart;
    public int health;

    private bool _isPause;
    private int Score => playerProfile.Score;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        AddEvents();
    }

    private void OnDestroy()
    {
        RemoveEvents();
    }

    private void AddEvents()
    {
        Player.Dead += OnPlayerDead;
        Player.UpdateHealth += OnPlayerUpdateHealth;
        Enemy.Dead += OnEnemyDead;
    }

    private void RemoveEvents()
    {
        Player.Dead -= OnPlayerDead;
        Player.UpdateHealth -= OnPlayerUpdateHealth;
        Enemy.Dead -= OnEnemyDead;
    }

    private void OnPlayerUpdateHealth(int health)
    {
        this.health = health;
        healthLabel.text = $"Health: {health}";
    }

    private void OnPlayerDead()
    {
        OnGameOver();
    }

    private void OnEnemyDead(int score)
    {
        playerProfile.SetScore(Score + score);
        scoreLabel.text = $"Score: {Score}";
    }

    private void Start()
    {
        _isPause = false;
        Time.timeScale = 1;
        inGameMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        scoreLabel.text = $"Score: {Score}";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPauseGame();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnBossEnemyAppear();
        }
    }

    private void OnBossEnemyAppear()
    {
        BossEnemyAppear?.Invoke();
        //AudioController.Instance.PlayBossBGM();
        DOTween.To(() => string.Empty, x => dialogueLabel.text = x, dialogueContent, dialogueDuration).Play();
    }

    public void OnPauseGame()
    {
        _isPause = !_isPause;
        if (_isPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        inGameMenu.SetActive(_isPause);
    }

    private void OnGameOver()
    {
        gameOverScoreLabel.text = $"Score: {Score}";
        gameOverMenu.SetActive(true);
    }

    public void OnClickBackToMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClickReplay()
    {
        Replay?.Invoke();
        playerProfile.SetScore(0);
        health = 0;
        gameOverMenu.SetActive(false);
    }
}
