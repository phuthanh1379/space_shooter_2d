using UnityEngine;

[CreateAssetMenu(fileName = "PlayerProfile", menuName = "Player/Profile")]
public class PlayerProfile : ScriptableObject
{
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    
    public int Score { get; private set; }

    public int CurrentHealth { get; private set; }

    public void SetCurrentHealth(int health)
    {
        CurrentHealth = health;
    }

    public void SetScore(int score)
    {
        Score = score;
    }
}