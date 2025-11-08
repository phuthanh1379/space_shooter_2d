using UnityEngine;

public class PlayerPlatformer : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        //var vertical = Input.GetAxisRaw("Vertical");

        transform.position += speed * Time.deltaTime * new Vector3(horizontal, 0f, 0f);
    }
}
