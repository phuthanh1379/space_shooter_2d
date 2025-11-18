using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;
using static UnityEngine.Rendering.GPUSort;

// Player
public class CardController : MonoBehaviour
{
    [SerializeField] private CardItem cardItemPrefab;
    [SerializeField] private RectTransform destination;

    [Header("Player")]
    [SerializeField] private RectTransform playerCardParent;
    [SerializeField] private float playerCardRotation;

    [Header("Opponent")]
    [SerializeField] private RectTransform opponentCardParent;
    [SerializeField] private float opponentCardRotation;

    public static event Action<CardItem, bool> CardPlayed;

    private List<CardItem> PlayerCards { get; } = new();
    private List<CardItem> OpponentCards { get; } = new();

    private bool _isPlayerTurn = false;

    private const float CardSpace = 20f;
    private const float CardPadding = 10f;

    private float CardParentWidth => playerCardParent.rect.width;

    private void Awake()
    {
        CardGameController.GameStarted += OnGameStarted;
        CardGameController.PlayerStarted += OnPlayerStarted;
        CardGameController.PlayerEnded += OnPlayerEnded;
        CardGameController.CardsDeal += OnCardsDeal;

        CardGameController.OpponentStarted += OnOpponentStarted;
    }

    private void OnDestroy()
    {
        CardGameController.GameStarted -= OnGameStarted;
        CardGameController.PlayerStarted -= OnPlayerStarted;
        CardGameController.PlayerEnded -= OnPlayerEnded;
        CardGameController.CardsDeal -= OnCardsDeal;

        CardGameController.OpponentStarted += OnOpponentStarted;
    }

    private void OnGameStarted()
    {
        //SpawnCards();
    }

    private void OnPlayerStarted()
    {
        _isPlayerTurn = true;
    }

    private void OnPlayerEnded()
    {
        _isPlayerTurn = false;
    }

    private void OnCardsDeal(Card[] playerCards, int dealCount)
    {
        SpawnCards(playerCards, true);
        SpawnOpponentCards(dealCount);
    }

    private void SpawnOpponentCards(int dealCount)
    {
        if (dealCount <= 0)
        {
            return;
        }

        SpawnCards(new Card[dealCount], false);
    }

    private void SpawnCards(Card[] cards, bool isPlayer)
    {
        if (cards == null || cards.Length <= 0)
        {
            return;
        }

        var count = cards.Length;
        var cardWidth = cardItemPrefab.GetWidth();
        var cardSpace = (CardParentWidth - cardWidth) / (count - 1);
        var startX = -(CardParentWidth - cardWidth) * 0.5f;
        var cardParent = isPlayer ? playerCardParent : opponentCardParent;
        var cardRotation = isPlayer ? playerCardRotation : opponentCardRotation;

        for (var i = 0; i < count; ++i)
        {
            var cardItem = Instantiate(cardItemPrefab, cardParent);
            cardItem.UpdateData(cards[i], i);
            cardItem.SetPosition(startX, 0);
            cardItem.SetRotation(cardRotation);
            startX += cardSpace;

            if (isPlayer)
            {
                HandlePlayer(cardItem);
            }
            else
            {
                HandleOpponent(cardItem);
            }
        }

        void HandlePlayer(CardItem cardItem)
        {
            cardItem.ItemClicked += OnCardItemClicked;
            cardItem.IsInteractable(true);
            PlayerCards.Add(cardItem);
        }

        void HandleOpponent(CardItem cardItem)
        {
            cardItem.IsInteractable(false);
            OpponentCards.Add(cardItem);
        }
    }

    private void ArrangeCards(bool isPlayer)
    {
        var cards = isPlayer ? PlayerCards : OpponentCards;
        var count = cards.Count;
        var cardWidth = cardItemPrefab.GetWidth();
        var cardSpace = (CardParentWidth - cardWidth) / (count - 1);
        var startX = -(CardParentWidth - cardWidth) * 0.5f;
        var sequence = DOTween.Sequence();
        
        for (var i = 0; i < count; ++i)
        {
            var cardItem = cards[i];
            sequence.Join(cardItem.MoveSimple(new Vector2(startX, 0), 0.5f));
            startX += cardSpace;
        }

        sequence.Play();
    }

    private void PlayCard(CardItem cardItem, bool isPlayer)
    {
        var cardParent = isPlayer ? playerCardParent : opponentCardParent;
        var positionOffset = destination.transform.position - cardParent.transform.position;
        var target = cardParent.InverseTransformVector(positionOffset);
        cardItem.SetCardParent(destination);
        cardItem
            .Move(target, 0.5f)
            .JoinCallback(ReArrangeCards)
            .OnComplete(() => CardPlayed?.Invoke(cardItem, isPlayer))
            .Play();

        void ReArrangeCards()
        {
            if (isPlayer)
            {
                PlayerCards.Remove(cardItem);
            }
            else
            {
                OpponentCards.Remove(cardItem);
            }

            ArrangeCards(isPlayer);
        }
    }

    private void OnCardItemClicked(CardItem cardItem)
    {
        if (!_isPlayerTurn)
        {
            UnityEngine.Debug.LogError("Not your turn yet!");
            return;
        }

        PlayCard(cardItem, true);
    }

    private void OnOpponentStarted()
    {
        if (OpponentCards == null || OpponentCards.Count <= 0)
        {
            return;
        }

        // Get a random card
        var index = new System.Random().Next(OpponentCards.Count);
        var cardItem = OpponentCards[index];
        var cardData = CardGameController.Instance.GetOpponentCard(index);
        cardItem.UpdateData(cardData, index);
        PlayCard(cardItem, false);
    }
}