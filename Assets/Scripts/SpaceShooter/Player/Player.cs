using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerProfile profile;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;
        [SerializeField] private Image healthBarImage;
        [SerializeField] private TMP_Text nameLabel;

        private int CurrentHealth => profile.CurrentHealth;
        private int MaxHealth => profile.Health;

        public static event Action Dead;
        public static event Action<int> UpdateHealth;

        private void OnGameReplay()
        {
            OnRevive();
        }

        private void OnHit()
        {
            StartCoroutine(OnGetHit());
            UpdateCurrentHealth(CurrentHealth - 1);
            if (CurrentHealth <= 0)
            {
                OnDead();
            }
        }

        private void OnDead()
        {
            Dead?.Invoke();
            animator.SetBool("IsDead", true);
        }

        public void OnRevive()
        {
            UpdateCurrentHealth(MaxHealth);
            animator.SetBool("IsDead", false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnHit();
        }

        private void Awake()
        {
            GameController.Replay += OnGameReplay;
        }

        private void OnDestroy()
        {
            GameController.Replay -= OnGameReplay;
        }

        private void Start()
        {
            UpdateCurrentHealth(MaxHealth);
            nameLabel.text = profile.Name;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("IsDead", false);
            }
        }

        // Player turns red when get hit,
        // then get back to normal color after x seconds
        private IEnumerator OnGetHit()
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.15f);
            spriteRenderer.color = Color.white;
        }

        private void UpdateCurrentHealth(int health)
        {
            profile.SetCurrentHealth(health);
            healthBarImage.fillAmount = (float)CurrentHealth / MaxHealth;
            UpdateHealth?.Invoke(CurrentHealth);
        }
    }
}