using Unity.VisualScripting;
using UnityEngine;

public class RepeatMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;

    private Vector3 _start;
    private Vector3 _end;
    private Vector3 _move;

    private void Start()
    {
        _start = startPosition;
        _end = endPosition;
        transform.position = startPosition;
        _move = endPosition - startPosition;
    }

    private void Update()
    {
        if (IsExceeded(transform.position, _end, _move))
        {
            _move *= -1;
            (_start, _end) = (_end, _start);
        }

        transform.Translate(speed * Time.deltaTime * _move);
    }

    private bool IsExceeded(Vector3 cur, Vector3 end, Vector3 move)
    {
        return IsExceeded(cur.x, end.x, move.x) &&
            IsExceeded(cur.y, end.y, move.y) &&
            IsExceeded(cur.z, end.z, move.z);
    }

    private bool IsExceeded(float cur, float max, float move)
    {
        if (move > 0)
        {
            if (cur >= max)
                return true;
            else
                return false;
        }

        if (move < 0)
        {
            if (cur <= max)
                return true;
            else
                return false;
        }

        return true;
    }
}
