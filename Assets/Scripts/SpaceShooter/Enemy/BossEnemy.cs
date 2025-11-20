using DG.Tweening;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public void Init(Vector3 startPosition, float duration)
    {
        transform.DOMove(startPosition, duration).Play();
    }
}
