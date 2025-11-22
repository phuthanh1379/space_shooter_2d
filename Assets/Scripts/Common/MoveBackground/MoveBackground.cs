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

    public void MultSpeed(float multiplier) => speed *= multiplier;

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