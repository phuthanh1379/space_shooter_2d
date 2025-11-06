using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    private int _currentHealth;
    public static event Action Dead;
    public static event Action<int> Hit;

    private void OnGameReplay()
    {
        OnRevive();
    }

    private void OnHit()
    {
        StartCoroutine(OnGetHit());
        _currentHealth -= 1; // fixed number, should get from Projectile script
        Hit?.Invoke(_currentHealth);
        if (_currentHealth <= 0)
        {
            OnDead();
        }
    }

    private void OnDead()
    {
        Dead?.Invoke();
        animator.SetBool("IsDead", true);
    }

    public void OnRevive()
    {
        _currentHealth = health;
        animator.SetBool("IsDead", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnHit();
    }

    private void Awake()
    {
        GameController.Replay += OnGameReplay;
    }

    private void OnDestroy()
    {
        GameController.Replay -= OnGameReplay;
    }

    private void Start()
    {
        _currentHealth = health;
    }

    private void Update()
    {
        GameController.Instance.health = _currentHealth;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("IsDead", false);
        }
    }

    // Player turns red when get hit,
    // then get back to normal color after x seconds
    private IEnumerator OnGetHit()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        spriteRenderer.color = Color.white;
    }
}