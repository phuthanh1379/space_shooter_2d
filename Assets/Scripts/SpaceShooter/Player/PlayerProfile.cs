using UnityEngine;

[CreateAssetMenu(fileName = "PlayerProfile", menuName = "Player/Profile")]
public class PlayerProfile : ScriptableObject
{
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public string Name { get; private set; }

    public int Score { get; private set; }

    //public int Score => PlayerPrefs.GetInt(ScoreKey, 0); // Example using PlayerPrefs
    //private const string ScoreKey = "Score"; // Example using PlayerPrefs

    public int CurrentHealth { get; private set; }

    public void SetCurrentHealth(int health)
    {
        CurrentHealth = health;
    }

    public void SetScore(int score)
    {
        Score = score;
        //PlayerPrefs.SetInt(ScoreKey, score); // Example using PlayerPrefs
    }
}