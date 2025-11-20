using System.Collections;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Start()
    {
        StartCoroutine(WaitToSelfDestruct(2.5f));
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.down);
    }

    private IEnumerator WaitToSelfDestruct(float time)
    {
        yield return new WaitForSeconds(time);
        SelfDestruct();
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
