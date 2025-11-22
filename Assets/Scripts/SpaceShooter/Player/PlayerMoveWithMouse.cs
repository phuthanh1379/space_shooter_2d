using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerMoveWithMouse : MonoBehaviour
    {
        [SerializeField] private float speed;

        private void Start()
        {
            // (Optional) Turn off cursor's visibility
            //Cursor.visible = false;
        }

        private void Update()
        {
            Vector3 mouseScreenPosition = Input.mousePosition;

            mouseScreenPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

            transform.position = mouseWorldPosition;
        }
    }
}
