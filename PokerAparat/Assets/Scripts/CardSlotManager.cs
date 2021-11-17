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
        PrepareCardSlots();
    }

    void Start()
    {
        EventManager.Instance.DealRoundChange.AddListener(OnDealRoundChangeHandler);
    }

    private void OnDestroy()
    {
        EventManager.Instance.DealRoundChange.RemoveListener(OnDealRoundChangeHandler);
    }

    private void OnDealRoundChangeHandler(CardDealer.DealRoundEnum newRound)
    {
        if (CardDealer.Instance.DealRound == CardDealer.DealRoundEnum.Zero)
        {
            CardDealer.Instance.DealRound = CardDealer.DealRoundEnum.First;
        }
        else if (CardDealer.Instance.DealRound == CardDealer.DealRoundEnum.First)
        {
            CardDealer.Instance.DealRound = CardDealer.DealRoundEnum.Second;
        }
        else
        {
            CardDealer.Instance.DealRound = CardDealer.DealRoundEnum.First;
        }

        if (CardDealer.Instance.DealRound == CardDealer.DealRoundEnum.First)
        {
            PlayerAccount.Instance.AddCredits(-BetManager.Instance.MyCurrentBet);
        }
    }

    

    void PrepareGambleScreen()
    {

    }

    public void DealCards()
    {
        StartCoroutine(DealCardsCoroutine());
    }

    IEnumerator DealCardsCoroutine()
    {
        GoalsManager.Instance.ShowWinCombinationBorder(GoalsManager.DefaultPrizes.Nothing);
        HashSet<Card.CardTypeEnum> _cardsNoLongerAvailable = ClearUnlockedCards();
        yield return StartCoroutine(DrawCards(_cardsNoLongerAvailable));

        List<Card> allCards = new List<Card>();
        foreach (CardSlot cs in _cardSlots)
        {
            if (cs.DrawnCard != null)
            {
                allCards.Add(cs.DrawnCard);
            }
        }

        EventManager.Instance.DealRoundChange.Invoke(CardDealer.Instance.DealRound);
        EventManager.Instance.EvaluateHand.Invoke(allCards, CardDealer.Instance.DealRound);
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

    IEnumerator DrawCards(HashSet<Card.CardTypeEnum> _cardsNoLongerAvailable)
    {
        if (_cardSlots == null || _cardSlots.Count == 0)
        {
            yield return null;
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

        yield return StartCoroutine(ShowCards(allAvailableCards, emptySlots));
    }

    private IEnumerator ShowCards(List<Card.CardTypeEnum> allAvailableCards, List<CardSlot> emptySlots)
    {
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

                // maybe animation of rotating the card?
                cse.CardBack.gameObject.SetActive(false);
                card.ParentCardSlot = cse;
            }
            yield return new WaitForSeconds(0.075f);
        }
    }
}
