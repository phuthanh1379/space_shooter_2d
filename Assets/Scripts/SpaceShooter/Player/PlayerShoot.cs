using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        [Header("Main gun")]
        [SerializeField] [Tooltip("Main projectile for main gun")] private Projectile projectile;
        [SerializeField] private Transform gunPoint;
        [SerializeField] private Animator muzzleAnimator;

        [Header("Secondary guns")]
        [SerializeField] private Projectile secondaryProjectile;
        [SerializeField] private Transform altGunPointLeft;
        [SerializeField] private Transform altGunPointRight;
        [SerializeField] private Animator leftMuzzleAnimator;
        [SerializeField] private Animator rightMuzzleAnimator;

        [Header("Gun Settings")]
        [SerializeField] private int maxBullets;
        [SerializeField] private float reloadDuration;

        private int _bulletCount;
        private float _reloadTime;
        private bool _isReloading;
        private bool _isShootable;

        private void Awake()
        {
            Player.Dead += OnPlayerDead;
            GameController.Replay += OnGameReplay;
        }

        private void OnDestroy()
        {
            Player.Dead -= OnPlayerDead;
            GameController.Replay -= OnGameReplay;
        }

        private void Start()
        {
            _isShootable = true;
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

        private void OnPlayerDead()
        {
            _isShootable = false;
        }

        private void OnGameReplay()
        {
            _isShootable = true;
            Reload();
        }

        private void ShootSecondary()
        {
            // Spawn new GameObject using Instantiate()
            SpawnSecondaryProjectile(altGunPointLeft, leftMuzzleAnimator);
            SpawnSecondaryProjectile(altGunPointRight, rightMuzzleAnimator);
        }

        private void SpawnSecondaryProjectile(Transform gunPointTransform, Animator gunAnim)
        {
            Instantiate(secondaryProjectile, gunPointTransform.position, gunPointTransform.rotation);
            gunAnim.Play("Muzzle_1", 0, 0f);
        }

        private void Shoot()
        {
            if (_isReloading || !_isShootable)
            {
                //Debug.LogError($"Gun is reloading! Time left: {_reloadTime}");
                return;
            }

            // Play sfx
            AudioController.Instance?.PlayLaserSFX();

            // Spawn new GameObject using Instantiate()
            var bullet = Instantiate(projectile);
            bullet.transform.position = gunPoint.position;
            _bulletCount++;
        
            // Invoke animation Fire from Player's animator
            //animator.SetTrigger("Fire");
            // Invoke animation Fire from Muzzle's animator
            muzzleAnimator.Play("Muzzle", 0, 0f);

            if (_bulletCount >= maxBullets)
            {
                _isReloading = true;
            }
        }

        private void Reload()
        {
            //Debug.LogError($"Reloaded!");
            _bulletCount = 0;
            _reloadTime = reloadDuration;
            _isReloading = false;
        }
    }
}
