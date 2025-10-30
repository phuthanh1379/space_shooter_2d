using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Vector3 _start;
    private Vector3 _end;
    private Vector3 _move;
    private float _speed;

    private bool _isMovable;

    public void SetMovable(bool isMovable)
    {
        _isMovable = isMovable;
    }

    public void Init(Vector3 start, Vector3 end, float speed)
    {
        _start = start;
        _end = end;
        transform.position = start;
        _move = end - start;
        _speed = speed;
        _isMovable = true;
    }

    private void Update()
    {
        if (!_isMovable)
        {
            return;
        }

        if (IsExceeded(transform.position, _end, _move))
        {
            _move *= -1;
            (_start, _end) = (_end, _start);
        }

        transform.Translate(_speed * Time.deltaTime * _move);
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
