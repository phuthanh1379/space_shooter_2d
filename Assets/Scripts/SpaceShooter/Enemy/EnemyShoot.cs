using System.Collections;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform gunPoint;
    [SerializeField] private float delayBetweenShots;
    [SerializeField] private int maxBullets;

    private float _timeCount;

    private void Update()
    {
        if (_timeCount <= 0)
        {
            Shoot();
            _timeCount = delayBetweenShots;
        }
        else
        {
            _timeCount -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        var count = new System.Random().Next(1, maxBullets);
        for (var i = 0; i < count; i++)
        {
            var rotationZ = new System.Random().Next(90, 270);
            var rotation = Quaternion.Euler(0f, 0f, rotationZ);
            var bullet = Instantiate(projectile, gunPoint.position, rotation);
        }
    }
}
