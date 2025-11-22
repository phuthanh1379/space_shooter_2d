using System;
using UnityEngine;

namespace CardGame
{
    [Serializable]
    public class Card
    {
        [field: SerializeField] public Sprite CardSprite
        {
            get; private set;
        }

        [field: SerializeField] public string CardName
        {
            get; private set; 
        }

        [field: SerializeField] public int CardValue
        {
            get; private set;
        }

        public Card(Sprite cardSprite, string cardName, int cardValue)
        {
            CardSprite = cardSprite;
            CardName = cardName;
            CardValue = cardValue;
        }
    }
}