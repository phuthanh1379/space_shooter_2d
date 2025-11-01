using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreLabel;
    [SerializeField] private TMP_Text healthLabel;

    public int score;
    public int health;

    private void Update()
    {
        scoreLabel.text = $"Score: {score}";
        healthLabel.text = $"Health: {health}";
    }
}
