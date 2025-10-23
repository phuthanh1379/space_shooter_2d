using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    [SerializeField] private Vector3 moveVector;

    private void Update()
    {
        transform.Translate(moveVector * Time.deltaTime);
    }
}
