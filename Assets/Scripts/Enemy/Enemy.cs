using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private EnemyMove move;

    private GameController gameController;

    public void Init(GameController gameController)
    {
        this.gameController = gameController;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Die();
    }

    private void Die()
    {
        gameController.score += 1;
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