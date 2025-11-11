using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Test : MonoBehaviour
{
    [SerializeField, Range(10, 100)] private int resolution;

    private void OnDrawGizmos()
    {
        for (var i = 0; i < resolution; i++)
        {
            var step = 2f / resolution;
            var x = (i + 0.5f) * step - 1f;
            var y = Mathf.Sin(Mathf.PI * (x + Time.time));
            Gizmos.DrawSphere(new Vector3(x, y, 0), 0.05f);
        }
    }
}
