using UnityEngine;

namespace Platformer
{
    public class PlatformerPlayer : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float speed;
        [SerializeField] private float jumpSpeed;

        [Header("Attack")]
        [SerializeField] private Transform attackCheck;
        [SerializeField] private float attackRadius;
        [SerializeField] private LayerMask enemyLayer;

        [Header("Ground check")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius;
        [SerializeField] private LayerMask groundLayer;

        [Header("Water")]
        [SerializeField] private LayerMask waterLayer;

        private Vector3 _baseScale;
        private float _horizontal;
        private float _vertical;
        private int _jumpCount;
        private bool _isGrounded;
        public bool _isInWater;

        private void OnAttack()
        {
            var collider = Physics2D.OverlapCircle(attackCheck.position, attackRadius, enemyLayer);
            if (collider.GetComponent<PlatformerEnemy>() != null)
            {
                collider.GetComponent<PlatformerEnemy>().OnHurt();
            }
        }

        private bool IsGrounded()
        {
            _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            if (_isGrounded)
            {
                _jumpCount = 0;
            }

            return _isGrounded;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            Gizmos.DrawWireSphere(attackCheck.position, attackRadius);
        }

        private void Awake()
        {
            _baseScale = transform.localScale;
        }

        private void Update()
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            animator.SetBool("Grounded", IsGrounded());
            animator.SetFloat("AirSpeed", rb.linearVelocityY);

            if (_isInWater)
            {
                _vertical = Input.GetAxisRaw("Vertical");
            }
            else
            {
                _vertical = 0f;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                animator.SetInteger("AnimState", (int)AnimationState.Combat);
                animator.SetTrigger("Attack");
            }
            else
            {
                if (_horizontal != 0)
                {
                    animator.SetInteger("AnimState", (int)AnimationState.Run);
                }
                else
                {
                    animator.SetInteger("AnimState", (int)AnimationState.Idle);
                }
            }

            if (_horizontal < 0f)
            {
                TurnLeft();
            }
            else if (_horizontal > 0f)
            {
                TurnRight();
            }
        }

        private void FixedUpdate()
        {
            rb.linearVelocityX = _horizontal * speed;
            if (_vertical != 0)
            {
                rb.linearVelocityY = _vertical * speed;
            }
        }

        private void Jump()
        {
            if (IsGrounded())
            {
                DoJump();
                _jumpCount++;
            }
            else if (_jumpCount == 1)
            {
                DoJump();
                _jumpCount++;
            }

            void DoJump()
            {
                animator.SetTrigger("Jump");
                rb.linearVelocityY = jumpSpeed;
            }
        }

        private void TurnLeft()
        {
            transform.localScale = _baseScale;
        }

        private void TurnRight()
        {
            transform.localScale = new Vector3(-_baseScale.x, _baseScale.y, _baseScale.z);
        }
    }

    public enum AnimationState
    {
        Idle = 0,
        Combat = 1,
        Run = 2,
    }
}