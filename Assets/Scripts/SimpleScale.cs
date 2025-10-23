using UnityEngine;

public class SimpleScale : MonoBehaviour
{
    [SerializeField] private float baseScale;
    [SerializeField] private float maxScale;
    [SerializeField] private float speed;

    private void Start()
    {
        transform.localScale = Vector3.one * baseScale;
    }

    private void Update()
    {
        if (transform.localScale.x >= maxScale || transform.localScale.x < baseScale)
        {
            speed *= -1;
        }

        transform.localScale += speed * Time.deltaTime * Vector3.one;
    }
}
