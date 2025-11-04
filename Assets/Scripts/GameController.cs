using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreLabel;
    [SerializeField] private TMP_Text healthLabel;
    [SerializeField] private GameObject inGameMenu;

    public static GameController Instance;
    public int score;
    public int health;

    private bool _isPause;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _isPause = false;
        Time.timeScale = 1;
        inGameMenu.SetActive(false);
    }

    private void Update()
    {
        scoreLabel.text = $"Score: {score}";
        healthLabel.text = $"Health: {health}";

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPauseGame();
        }
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

    public void OnClickBackToMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
