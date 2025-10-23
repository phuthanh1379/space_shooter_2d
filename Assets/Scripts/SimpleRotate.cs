using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    [SerializeField] private float selfRotateSpeed;

    private void Update()
    {
        var axis = Vector3.forward;
        transform.Rotate(axis, selfRotateSpeed);
    }
}
