using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RepeatMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private List<Vector3> destinations = new();

    private bool _isMovable;
    private int _currentIndex;
    private Vector3 _currentPosition;
    private Vector3 _nextPosition;
    private Vector3 _move;

    public void SetDestinations(List<Vector3> destinations)
    {
        this.destinations = new List<Vector3>(destinations);
    }

    private void Start()
    {
        if (destinations == null || destinations.Count <= 1)
        {
            _isMovable = false;
            return;
        }

        _isMovable = true;
        _currentIndex = 0;
        MoveNext();
    }

    private void Update()
    {
        if (!_isMovable)
        {
            return;
        }

        if (IsExceeded(transform.position, _nextPosition, _move))
        {
            //_move *= -1;
            //(_currentPosition, _nextPosition) = (_nextPosition, _currentPosition);
            MoveNext();
        }

        transform.Translate(speed * Time.deltaTime * _move);
    }

    private void MoveNext()
    {
        if (_currentIndex >= destinations.Count)
        {
            _currentIndex = 0;
        }

        var next = _currentIndex + 1;
        if (next >= destinations.Count)
        {
            next = 0;
        }
        _currentPosition = destinations[_currentIndex];
        _nextPosition = destinations[next];
        transform.position = _currentPosition;
        _move = _nextPosition - _currentPosition;
        _currentIndex += 1;
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
