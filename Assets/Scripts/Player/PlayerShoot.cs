using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private Projectile secondaryProjectile;

    [SerializeField] private Transform gunPoint;
    [SerializeField] private Transform altGunPointLeft;
    [SerializeField] private Transform altGunPointRight;
    [SerializeField] private int maxBullets;
    [SerializeField] private float reloadDuration;

    private int _bulletCount;
    private float _reloadTime;
    private bool _isReloading;

    private void Start()
    {
        Reload();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetMouseButtonDown(1))
        {
            ShootSecondary();
        }

        if (_reloadTime <= 0)
        {
            Reload();
        }

        if (_isReloading)
        {
            _reloadTime -= Time.deltaTime;
        }
    }

    private void ShootSecondary()
    {
        // Spawn new GameObject using Instantiate()
        SpawnSecondaryProjectile(altGunPointLeft);
        SpawnSecondaryProjectile(altGunPointRight);
    }

    private void SpawnSecondaryProjectile(Transform gunPointTransform)
    {
        Instantiate(secondaryProjectile, gunPointTransform.position, gunPointTransform.rotation);
    }

    private void Shoot()
    {
        if (_isReloading)
        {
            Debug.LogError($"Gun is reloading! Time left: {_reloadTime}");
            return;
        }

        // Spawn new GameObject using Instantiate()
        var bullet = Instantiate(projectile);
        bullet.transform.position = gunPoint.position;
        _bulletCount++;
        Debug.LogError($"Shot Fired! Bullet count={_bulletCount}");

        if (_bulletCount >= maxBullets)
        {
            _isReloading = true;
        }
    }

    private void Reload()
    {
        Debug.LogError($"Reloaded!");
        _bulletCount = 0;
        _reloadTime = reloadDuration;
        _isReloading = false;
    }
}
