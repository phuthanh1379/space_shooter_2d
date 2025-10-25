using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;

    // SOLUTION 2: Countdown duration in Update
    // private float duration = 2f;

    private void Start()
    {
        StartCoroutine(WaitToSelfDestruct(2f));
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.up);

        // SOLUTION 2: Countdown duration in Update
        //duration -= Time.deltaTime;
        //if (duration <= 0)
        //{
        //    SelfDestruct();
        //}
    }

    // SOLUTION 1: Countdown duration with Unity Coroutine
    private IEnumerator WaitToSelfDestruct(float time)
    {
        Debug.Log("START Coroutine");
        // Will wait for <time> seconds then continue the process
        yield return new WaitForSeconds(time);
        Debug.Log("END Coroutine");
        SelfDestruct();
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}