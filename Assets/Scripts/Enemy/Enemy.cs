using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private EnemyMove move;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Die();
    }

    private void Die()
    {
        animator.SetTrigger("Die");
        boxCollider.enabled = false;
        move.SetMovable(false);
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