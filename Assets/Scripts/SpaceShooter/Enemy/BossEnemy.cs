using DG.Tweening;
using UnityEngine;

namespace SpaceShooter.Enemy
{
    public class BossEnemy : MonoBehaviour
    {
        public void Init(Vector3 startPosition, float duration)
        {
            transform.DOMove(startPosition, duration).Play();
        }
    }
}
