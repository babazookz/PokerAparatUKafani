using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleOrNothingSlot : MonoBehaviour
{
    public Card DrawnCard;
    public Image CardImage;
    private Sprite _defaultSprite;

    private void Awake()
    {
        _defaultSprite = CardImage.sprite;
    }

    private void OnDisable()
    {
        CardImage.sprite = _defaultSprite;
    }

    public void PrepareCard(Card _card)
    {
        DrawnCard = _card;
        CardImage.sprite = DrawnCard.CardSprite;
    }
}
