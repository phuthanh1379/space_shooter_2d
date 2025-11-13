using UnityEngine;

public class MoveBackgroundVertical : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxY;
    [SerializeField] private Vector3 basePosition;

    private void Update()
    {
        if (transform.position.y <= maxY)
        {
            transform.position = basePosition;
        }

        transform.Translate(speed * Time.deltaTime * Vector3.down);
    }
}