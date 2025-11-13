using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider;
    //[SerializeField] private EnemyMove move;
    [SerializeField] private RepeatMove move;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int score;

    public static event Action<int> Dead;

    public void Init(Sprite sprite, int score, List<Vector3> positions)
    {
        spriteRenderer.sprite = sprite;
        this.score = score;
        move.SetDestinations(positions);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Die();
    }

    public void Die()
    {
        spriteRenderer.gameObject.SetActive(false);
        Dead?.Invoke(score);
        animator.SetTrigger("Die");
        boxCollider.enabled = false;
        //move.SetMovable(false);
    }

    private void SelfDestruct()
    {
        Destroy(this.gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}