using UnityEngine;

public class PlatformerEnemy : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void OnHurt()
    {
        animator.SetTrigger("Hurt");
    }
}
