using UnityEngine;

public class BossEnemyShoot : MonoBehaviour
{
    [SerializeField] private Transform gunPoint;
    [SerializeField] private BossProjectile specialProjectile;
    [SerializeField] private Projectile projectile;
    [SerializeField] private float delayBetweenShots;
    [SerializeField] private int maxBullets;

    private float _timeCount;
    private bool _hasUsedSpecial;

    private void Start()
    {
        _hasUsedSpecial = false;
    }

    private void Update()
    {
        if (_timeCount <= 0)
        {
            if (!_hasUsedSpecial)
            {
                ShootSpecial();
            }
            else
            {
                Shoot();
            }

            _timeCount = delayBetweenShots;
        }
        else
        {
            _timeCount -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        _hasUsedSpecial = false;
        var count = new System.Random().Next(1, maxBullets);
        for (var i = 0; i < count; i++)
        {
            var rotationZ = new System.Random().Next(90, 270);
            var rotation = Quaternion.Euler(0f, 0f, rotationZ);
            var bullet = Instantiate(projectile, gunPoint.position, rotation);
        }
    }

    public void ShootSpecial()
    {
        var bullet = Instantiate(specialProjectile, gunPoint.position, Quaternion.Euler(Vector3.down));
        AudioController.Instance.PlayExplosionSFX();
        _hasUsedSpecial = true;
    }
}
