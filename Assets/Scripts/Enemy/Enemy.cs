using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerDamage"))
        {
            Destroy(this.gameObject);
            //gameObject.SetActive(false);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}