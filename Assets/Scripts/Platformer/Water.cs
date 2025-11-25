using Platformer;
using Unity.VisualScripting;
using UnityEngine;

public class Water : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlatformerPlayer>() != null)
        {
            collision.GetComponent<PlatformerPlayer>()._isInWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlatformerPlayer>() != null)
        {
            collision.GetComponent<PlatformerPlayer>()._isInWater = false;
        }
    }
}
