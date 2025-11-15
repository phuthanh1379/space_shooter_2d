using System;
using UnityEngine;

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

    public Card(Sprite cardSprite, string cardName)
    {
        CardSprite = cardSprite;
        CardName = cardName;
    }
}