using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float selfRotateSpeed;
    [SerializeField] private Transform pointGameObject;

    private void Update()
    {
        var axis = Vector3.forward;
        var point = pointGameObject.position;
        transform.RotateAround(point, axis, rotateSpeed);
        transform.Rotate(axis, selfRotateSpeed);
    }
}
