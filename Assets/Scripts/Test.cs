using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Test : MonoBehaviour
{
    [SerializeField] private Transform target;
    //[SerializeField, Range(10, 100)] private int resolution;

    private Transform basePosition;
    private float duration = 3f;
    private float elapsed = 0f;

    private void Start()
    {
        basePosition = transform;
    }

    //private void OnDrawGizmos()
    //{
    //    for (var i = 0; i < resolution; i++)
    //    {
    //        var step = 2f / resolution;
    //        var x = (i + 0.5f) * step - 1f;
    //        var y = Mathf.Sin(Mathf.PI * (x + Time.time));
    //        Gizmos.DrawSphere(new Vector3(x, y, 0), 0.05f);
    //    }
    //}

    private void Update()
    {
        elapsed += Time.deltaTime;

        transform.position = Vector3.Lerp(basePosition.position, target.position, Time.deltaTime);
    }
}
