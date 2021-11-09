using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CardSlotManager : MonoBehaviour
{
    private static CardSlotManager _instance;
    public Transform CardSlotParent;
    private List<CardSlot> _cardSlots;
    public Button DrawCardsButton;

    public static CardSlotManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        _cardSlots = new List<CardSlot>();
        DrawCardsButton.onClick.AddListener(DealCards);
        PrepareCardSlots();
    }

    void DealCards()
    {
        HashSet<Card.CardTypeEnum> _cardsNoLongerAvailable = ClearUnlockedCards();
        DrawCards(_cardsNoLongerAvailable);

        List<Card> allCards = new List<Card>();
        foreach (CardSlot cs in _cardSlots)
        {
            if (cs.DrawnCard != null)
            {
                allCards.Add(cs.DrawnCard);
            }
        }

        if (CardDealer.Instance.DealRound == CardDealer.DealRoundEnum.Zero)
        {
            CardDealer.Instance.DealRound = CardDealer.DealRoundEnum.First;
            HandEvaluator.Instance.EvaluateHand(allCards, false);
        }
        else if (CardDealer.Instance.DealRound == CardDealer.DealRoundEnum.First)
        {
            CardDealer.Instance.DealRound = CardDealer.DealRoundEnum.Second;
            HandEvaluator.Instance.EvaluateHand(allCards);
        }
        else
        {
            CardDealer.Instance.DealRound = CardDealer.DealRoundEnum.First;
            HandEvaluator.Instance.EvaluateHand(allCards, false);
        }

        if (CardDealer.Instance.DealRound == CardDealer.DealRoundEnum.First)
        {
            PlayerAccount.Instance.AddCredits(-BetManager.Instance.MyCurrentBet);
        }


    }

    void PrepareCardSlots()
    {
        if (CardSlotParent == null)
            return;

        Transform[] cardSlots = CardSlotParent.GetComponentsInChildren<Transform>();

        if (cardSlots == null)
            return;

        foreach (Transform slot in cardSlots)
        {
            CardSlot cs = slot.gameObject.GetComponent<CardSlot>();

            if (cs != null)
                _cardSlots.Add(cs);
        }
    }

    private HashSet<Card.CardTypeEnum> ClearUnlockedCards()
    {
        HashSet<Card.CardTypeEnum> cardsNoLongerAvailable = new HashSet<Card.CardTypeEnum>();
        if (_cardSlots == null || _cardSlots.Count == 0)
        {
            return cardsNoLongerAvailable;
        }

        foreach (CardSlot cs in _cardSlots)
        {
            if (CardDealer.Instance.DealRound == CardDealer.DealRoundEnum.First)
            {
                if (!cs.IsLocked && cs.DrawnCard != null)
                {
                    cs.DrawnCard.gameObject.SetActive(false);
                    cs.DrawnCard.transform.SetParent(CardDealer.Instance.ObjectTransform, false);
                    cardsNoLongerAvailable.Add(cs.DrawnCard.CardType);
                    cs.DrawnCard = null;
                }
            }
            else
            {
                if (cs.DrawnCard == null)
                    continue;

                cs.DrawnCard.gameObject.SetActive(false);
                cs.DrawnCard.transform.SetParent(CardDealer.Instance.ObjectTransform, false);
                cs.DrawnCard = null;
                cs.ResetSlot();
            }
        }

        return cardsNoLongerAvailable;
    }

    void DrawCards(HashSet<Card.CardTypeEnum> _cardsNoLongerAvailable)
    {
        if (_cardSlots == null || _cardSlots.Count == 0)
        {
            return;
        }

        HashSet<Card.CardTypeEnum> cardTypeDrawn = new HashSet<Card.CardTypeEnum>();
        List<CardSlot> emptySlots = new List<CardSlot>();
        List<Card.CardTypeEnum> allAvailableCards = CardDealer.Instance.CardDictionary.Keys.ToList();

        foreach (Card.CardTypeEnum card in _cardsNoLongerAvailable)
        {
            allAvailableCards.Remove(card);
        }

        foreach (CardSlot cs in _cardSlots)
        {
            if (cs.DrawnCard != null)
            {
                cardTypeDrawn.Add(cs.DrawnCard.CardType);
                allAvailableCards.Remove(cs.DrawnCard.CardType);
            }
            else
            {
                emptySlots.Add(cs);
            }
        }

        foreach (CardSlot cse in emptySlots)
        {
            Card.CardTypeEnum randomAvailableCard = allAvailableCards[Random.Range(0, allAvailableCards.Count)];

            if (CardDealer.Instance.CardDictionary.ContainsKey(randomAvailableCard))
            {
                Card card = CardDealer.Instance.CardDictionary[randomAvailableCard];
                allAvailableCards.Remove(randomAvailableCard);
                cse.DrawnCard = card;
                card.gameObject.SetActive(true);
                card.transform.SetParent(cse.transform, false);
                cse.CardBack.gameObject.SetActive(false);
                card.ParentCardSlot = cse;
            }
        }
    }
}
