using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private GameController gameController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(OnGetHit());
        health -= 1; // fixed number, should get from Projectile script
        if (health <= 0)
        {
            animator.SetBool("IsDead", true);
        }
    }

    private void Update()
    {
        gameController.health = health;
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