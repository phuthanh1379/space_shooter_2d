using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image image;
    [SerializeField] private Image cardBack;
    [SerializeField] private TMP_Text nameText;

    private bool _isInteractable;
    private int _index;
    private Card data;
    private Vector2 _basePosition;

    public event Action<CardItem> ItemClicked;

    public void IsInteractable(bool isInteractable)
        => _isInteractable = isInteractable;

    public void SetPosition(float x, float y)
        => rectTransform.anchoredPosition = new Vector2(x, y);

    public void SetRotation(float z)
        => rectTransform.rotation = Quaternion.Euler(0f, 0f, z);

    public void SetCardParent(Transform cardParent)
        => rectTransform.SetParent(cardParent);

    public float GetWidth() => rectTransform.rect.width;
    public float GetHeight() => rectTransform.rect.height;
    public string GetCardName() => nameText.text;
    public Card GetData() => data;

    public Sequence Move(Vector2 destination, float duration)
    {
        DOTween.Kill(this);
        var move = rectTransform.DOAnchorPos(destination, duration);
        var rotate = rectTransform.DORotate(new Vector3(0f, 0f, new System.Random().Next(-90, 90)), duration);
        var scale = rectTransform.DOScale(0.5f, duration);

        return DOTween.Sequence()
            .Append(move)
            .Join(rotate)
            .Join(scale)
            ;
    }

    public Tween MoveSimple(Vector2 target, float duration)
    {
        return rectTransform.DOAnchorPos(target, duration);
    }

    //public void Move(Vector3 worldSpaceDestination, float duration)
    //{
    //    //var target = rectTransform.InverseTransformVector(worldSpaceDestination);
    //    rectTransform.DOMove(worldSpaceDestination, duration).Play();
    //}

    private void Start()
    {
        _basePosition = rectTransform.anchoredPosition;
    }

    public void UpdateData(Card cardData, int index)
    {
        if (cardData == null)
        {
            cardBack.gameObject.SetActive(true);
            return;
        }

        cardBack.gameObject.SetActive(false);
        _index = index;
        gameObject.name = $"Card {index}";
        transform.SetSiblingIndex(index);
        data = cardData;
        image.sprite = cardData.CardSprite;
        nameText.text = $"{cardData.CardName}: {cardData.CardValue}";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isInteractable)
        {
            return;
        }

        // Move card up 20f
        rectTransform.DOAnchorPosY(_basePosition.y + 50f, 0.5f)
            .SetTarget(this)
            .Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_isInteractable)
        {
            return;
        }

        // Move card back to base position
        rectTransform.DOAnchorPosY(_basePosition.y, 0.5f)
            .SetTarget(this)
            .Play();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_isInteractable)
        {
            return;
        }

        ItemClicked?.Invoke(this);
    }
}