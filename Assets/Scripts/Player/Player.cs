using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private const string EnemyDamageTag = "EnemyDamage";
    private const string EnemyTag = "Enemy";
    private const string MeteorTag = "Meteor";

    private bool IsHit(GameObject target)
    {
        return target.CompareTag(EnemyDamageTag) || target.CompareTag(EnemyTag) || target.CompareTag(MeteorTag);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsHit(collision.gameObject))
        {
            StartCoroutine(OnGetHit());   
            health -= 1; // fixed number, should get from Projectile script
            if (health <= 0)
            {
                Destroy(gameObject);
                UnityEngine.Debug.LogError("GAME OVER!");
            }
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