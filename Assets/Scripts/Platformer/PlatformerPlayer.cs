using UnityEngine;

namespace Platformer
{
    public class PlatformerPlayer : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float speed;
        [SerializeField] private float jumpSpeed;

        [Header("Ground check")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius;
        [SerializeField] private LayerMask groundLayer;

        private Vector3 _baseScale;
        private float _horizontal;
        private int _jumpCount;

        private bool IsGrounded()
        {
            var isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            if (isGrounded)
            {
                _jumpCount = 0;
            }

            return isGrounded;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        private void Awake()
        {
            _baseScale = transform.localScale;
        }

        private void Update()
        {
            _horizontal = Input.GetAxisRaw("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            // Do mac dinh sprite nhan vat quay ve phia ben phai*
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
                rb.linearVelocityY = jumpSpeed;
            }
        }

        private void TurnLeft()
        {
            transform.localScale = new Vector3(-_baseScale.x, _baseScale.y, _baseScale.z);
        }

        private void TurnRight()
        {
            transform.localScale = _baseScale;
        }
    }
}