using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardGameData", menuName = "Card Game/CardGameData")]
public class CardGameData : ScriptableObject
{
    [SerializeField] private List<Card> cards = new();
    [SerializeField] private int cardDealQuantity;

    public List<Card> Cards => cards;
    public int DealQuantity => cardDealQuantity;
}