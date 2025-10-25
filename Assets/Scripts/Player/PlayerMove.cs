using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject leftPipeGameObject;
    [SerializeField] private GameObject rightPipeGameObject;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        if (vertical != 0)
        {
            leftPipeGameObject.SetActive(true);
            rightPipeGameObject.SetActive(true);
        }
        else
        {
            if (horizontal > 0)
            {
                leftPipeGameObject.SetActive(true); // Show gameobject
                rightPipeGameObject.SetActive(false); // Hide gameobject
            }
            else if (horizontal < 0)
            {
                leftPipeGameObject.SetActive(false); // Hide gameobject
                rightPipeGameObject.SetActive(true); // Show gameobject
            }
            else
            {
                leftPipeGameObject.SetActive(false); // Hide gameobject
                rightPipeGameObject.SetActive(false); // Hide gameobject
            }
        }

        transform.position += speed * Time.deltaTime * new Vector3(horizontal, vertical, 0f);
    }
}