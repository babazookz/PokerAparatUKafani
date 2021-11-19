using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DoubleOrNothingManager : MonoBehaviour
{
    private bool userSelectedLowCard;
    private static DoubleOrNothingManager _instance;
    private List<GameObject> drawnCards = new List<GameObject>();
    public int StartingCardOffset = 20;
    public int SpaceBetweenCards = 50;
    private List<Card.CardTypeEnum> allAvailableCards = null;
    private Card _lastDrawnCard = null;
    private int _currentPrizeWon = 0;

    public static DoubleOrNothingManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void PrepareDoubleOrNothingData(int currentPrize)
    {
        if (allAvailableCards == null)
            allAvailableCards = CardDealer.Instance.CardDictionary.Keys.ToList();

        _currentPrizeWon = currentPrize;
    }

    public void DoubleOrNothingFinishAction()
    {
        ClearDrawnCards();
    }

    private void ShowCard(DoubleOrNothingSlot dns)
    {
        Card.CardTypeEnum randomAvailableCard = allAvailableCards[Random.Range(0, allAvailableCards.Count)];

        if (CardDealer.Instance.CardDictionary.ContainsKey(randomAvailableCard))
        {
            Card card = CardDealer.Instance.CardDictionary[randomAvailableCard];
            allAvailableCards.Remove(randomAvailableCard);
            dns.PrepareCard(card);
            _lastDrawnCard = card;
            AudioManager.Instance.PlayMainAudioSourceClip(AudioManager.Instance.CardFlip);
        }
    }

    public bool CheckIsItCorrect()
    {
        if (_lastDrawnCard.SuitType == Card.Suit.Joker)
            return true;

        if(userSelectedLowCard)
        {
            return _lastDrawnCard.CardValue < 8;
        }
        else
        {
            return _lastDrawnCard.CardValue >= 8;
        }
    }

    public void DrawNewCard(bool isLowCard)
    {
        if (drawnCards.Count == 15)
        {
            ClearDrawnCards();
        }

        userSelectedLowCard = isLowCard;
        CreateNewCardItem();
        
        if(CheckIsItCorrect())
        {
            _currentPrizeWon *= 2;
            UIManager.Instance.UpdateDoubleOrNothingAmount(_currentPrizeWon);
        }
        else
        {
            _currentPrizeWon *= 0;
            UIManager.Instance.UpdateDoubleOrNothingAmount(_currentPrizeWon);
            EventManager.Instance.GamblingOver.Invoke();
        }
    }

    public int HalfThePrize()
    {
        _currentPrizeWon /= 2;
        PlayerAccount.Instance.AddCredits(_currentPrizeWon);
        return _currentPrizeWon;
    }

    void CreateNewCardItem()
    {
        GameObject cardPrefab = PoolManager.Instance.PoolOut(PoolManager.Instance.CardItem);
        cardPrefab.transform.SetParent(UIManager.Instance.DoubleOrNothingPanel, false);
        RectTransform rt = cardPrefab.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(StartingCardOffset + (drawnCards.Count * SpaceBetweenCards), rt.anchoredPosition.y);
        DoubleOrNothingSlot dns = cardPrefab.GetComponent<DoubleOrNothingSlot>();
        ShowCard(dns);
        drawnCards.Add(cardPrefab);
    }

    private void ClearDrawnCards()
    {
        foreach (GameObject rowGO in drawnCards)
        {
            rowGO.transform.SetParent(null, false);
            PoolManager.Instance.PoolIn(rowGO);
        }
        drawnCards.Clear();
    }
}
