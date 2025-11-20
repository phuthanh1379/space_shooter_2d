using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    [SerializeField] private Transform[] transforms;
    [SerializeField] private float radius;

    public List<Vector3> PositionList { get; } = new List<Vector3>();

    private void OnDrawGizmos()
    {
        foreach (Transform t in transforms)
        {
            Gizmos.DrawWireCube(t.position, Vector3.one * 0.5f);
        }

        PositionList.Clear();
        for (float t = 0; t <= 1; t += 0.025f)
        {
            var pos = Mathf.Pow(1 - t, 3) * transforms[0].position +
                3 * Mathf.Pow(1 - t, 2) * t * transforms[1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * transforms[2].position +
                Mathf.Pow(t, 3) * transforms[3].position;

            PositionList.Add(pos);
            Gizmos.DrawSphere(pos, radius);
        }

        Gizmos.DrawLine(transforms[0].position, transforms[1].position);
        Gizmos.DrawLine(transforms[2].position, transforms[3].position);
    }
}
