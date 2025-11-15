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

        //var maxCardSpace = cardWidth + CardSpace;
        //if (count % 2 != 0)
        //{
        //    var middleIndex = count / 2;
        //    _cardItemList[middleIndex].SetPosition(0, 0);

        //    if (count == 1)
        //    {
        //        return;
        //    }

        //    // Go backward
        //    for (var i = middleIndex - 1; i >= 0; --i)
        //    {
        //        var x = -maxCardSpace * (middleIndex - i);
        //        _cardItemList[i].SetPosition(x, 0);
        //    }

        //    // Go forward
        //    for (var i = middleIndex + 1; i < count; ++i)
        //    {
        //        var x = maxCardSpace * (i - middleIndex);
        //        _cardItemList[i].SetPosition(x, 0);
        //    }
        //}
    }

    private void OnCardItemClicked(CardItem cardItem)
    {
        //var positionOffset = destination.transform.position - cardItem.transform.position;
        cardItem.SetCardParent(destination);
        cardItem.Move(Vector2.zero, 0.5f).Play();
    }
}