using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardGameData", menuName = "Card Game/CardGameData")]
public class CardGameData : ScriptableObject
{
    [SerializeField] private List<Card> cards = new();

    public List<Card> Cards => cards;
}