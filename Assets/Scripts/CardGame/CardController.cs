using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField] private CardGameData gameData;
    [SerializeField] private CardItem cardItemPrefab;
    [SerializeField] private RectTransform cardParent;
    [SerializeField] private RectTransform destination;

    private List<CardItem> _cardItemList = new();

    private const float CardSpace = 20f;
    private const float CardPadding = 10f;

    private float CardParentWidth => cardParent.rect.width;

    private void Start()
    {
        UpdateData();
    }

    private void UpdateData()
    {
        if (gameData == null)
        {
            return;
        }

        var cards = gameData.Cards;
        var count = cards.Count;
        var cardWidth = cardItemPrefab.GetWidth();
        var cardSpace = (CardParentWidth - cardWidth) / (count - 1);
        var startX = -(CardParentWidth - cardWidth) * 0.5f;

        for (var i = 0; i < count; ++i)
        {
            var cardItem = Instantiate(cardItemPrefab, cardParent);
            cardItem.UpdateData(cards[i], i);
            cardItem.SetPosition(startX, 0);
            cardItem.ItemClicked += OnCardItemClicked;
            _cardItemList.Add(cardItem);
            startX += cardSpace;
        }
    }

    private void OnCardItemClicked(CardItem cardItem)
    {
        //var positionOffset = destination.transform.position - cardItem.transform.position;
        cardItem.SetCardParent(destination);
        cardItem.Move(Vector2.zero, 0.5f).Play();
    }
}