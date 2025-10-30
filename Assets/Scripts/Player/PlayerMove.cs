using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject pipeGameObject;
    [SerializeField] private Animator animator;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        animator.SetInteger("Horizontal", (int)horizontal);

        // If player is moving
        if (vertical != 0 || horizontal != 0)
        {
            pipeGameObject.SetActive(true);
        }
        else
        {
            pipeGameObject.SetActive(false);
        }

        transform.position += speed * Time.deltaTime * new Vector3(horizontal, vertical, 0f);
    }
}