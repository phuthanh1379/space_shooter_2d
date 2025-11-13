using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    private enum Direction
    {
        Left,
        Right,
    }

    [SerializeField] private Direction direction;
    [SerializeField] private float speed;
    [SerializeField] private float maxX;
    [SerializeField] private Vector3 basePosition;

    private void Update()
    {
        if (direction == Direction.Left)
        {
            if (transform.position.x <= maxX)
            {
                transform.position = basePosition;
            }

            transform.Translate(speed * Time.deltaTime * Vector3.left);
        }
        else
        {
            if (transform.position.x >= maxX)
            {
                transform.position = basePosition;
            }

            transform.Translate(speed * Time.deltaTime * Vector3.right);
        }
    }
}