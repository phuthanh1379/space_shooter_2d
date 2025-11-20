using UnityEngine;

[CreateAssetMenu(fileName = "EnemyProfile", menuName = "Enemies/Profile")]
public class EnemyProfile : ScriptableObject
{
    [field: SerializeField]
    public Sprite ModelSprite
    {
        get; private set;
    }
}
