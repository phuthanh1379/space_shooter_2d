using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject pipeGameObject;
    [SerializeField] private Animator animator;

    private bool _isMovable;

    private void Awake()
    {
        Player.Dead += OnPlayerDead;
        GameController.Replay += OnGameReplay;
        GameController.BossEnemyAppear += OnBossEnemyAppear;
        GameController.BossEnemyFightStart += OnBossEnemyFightStart;
    }

    private void OnDestroy()
    {
        Player.Dead -= OnPlayerDead;
        GameController.Replay -= OnGameReplay;
        GameController.BossEnemyAppear -= OnBossEnemyAppear;
        GameController.BossEnemyFightStart -= OnBossEnemyFightStart;
    }

    private void Start()
    {
        _isMovable = true;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!_isMovable)
        {
            return;
        }

        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        animator.SetInteger("Horizontal", (int)horizontal);

        // If player is moving
        if (vertical != 0 || horizontal != 0)
        {
            pipeGameObject.SetActive(true);
        }
        else
        {
            pipeGameObject.SetActive(false);
        }

        transform.position += speed * Time.deltaTime * new Vector3(horizontal, vertical, 0f);
    }

    private void OnPlayerDead()
    {
        _isMovable = false;
    }

    private void OnGameReplay()
    {
        _isMovable = true;
    }

    private void OnBossEnemyAppear()
    {
        _isMovable = false;
    }

    private void OnBossEnemyFightStart()
    {
        _isMovable = true;
    }
}