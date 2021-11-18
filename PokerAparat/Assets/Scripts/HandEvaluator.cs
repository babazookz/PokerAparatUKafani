using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HandEvaluator : MonoBehaviour
{
    private static HandEvaluator _instance;
    private int heartTotal;
    private int diamondTotal;
    private int clubTotal;
    private int spadeTotal;
    private int jokerTotal;
    private Dictionary<int, int> CardValueCountDictionary = new Dictionary<int, int>();

    public static HandEvaluator Instance
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

    private void Start()
    {
        EventManager.Instance.EvaluateHand.AddListener(OnEvaluateHandHandler);
    }

    private void OnDestroy()
    {
        EventManager.Instance.EvaluateHand.RemoveListener(OnEvaluateHandHandler);
    }

    private void OnEvaluateHandHandler(List<Card> allCards, CardDealer.DealRoundEnum currentRound)
    {
        EvaluateHand(allCards, currentRound);
    }

    public void AddPrize(int prizeAmount)
    {
        PlayerAccount.Instance.AddCredits(prizeAmount * BetManager.Instance.MyCurrentBet);
    }

    public void EvaluateHand(List<Card> cards, CardDealer.DealRoundEnum currentRound)
    {
        GoalsManager.Instance.ShowWinCombinationBorder(GoalsManager.DefaultPrizes.Nothing);
        CardValueCountDictionary.Clear();
        heartTotal = 0;
        diamondTotal = 0;
        clubTotal = 0;
        spadeTotal = 0;
        jokerTotal = 0;

        int prizeAmount = 0;
        string winningCombination = string.Empty;

        // sort by card value
        cards = cards.OrderBy(ob => ob.CardValue).ToList();

        foreach (Card element in cards)
        {
            element.ParentCardSlot.ResetSlot();
        }

        AnalyzeCards(cards);

        if (IsFiveOfAKind(cards))
        {
            Debug.Log("it is FIVE OF A KIND");
            prizeAmount = (int)GoalsManager.DefaultPrizes.FiveOfKind;
            GoalsManager.Instance.ShowWinCombinationBorder(GoalsManager.DefaultPrizes.FiveOfKind);
            winningCombination = GoalsManager.Instance.WinCombinationDictionary[GoalsManager.WinCombination.FIVE_OF_A_KIND];
        }
        else if (IsRoyalFlush(cards))
        {
            Debug.Log("it is ROYAL FLUSH");
            prizeAmount = (int)GoalsManager.DefaultPrizes.RoyalFlush;
            GoalsManager.Instance.ShowWinCombinationBorder(GoalsManager.DefaultPrizes.RoyalFlush);
            winningCombination = GoalsManager.Instance.WinCombinationDictionary[GoalsManager.WinCombination.ROYAL_FLUSH];
        }
        else if (IsStraightFlush(cards))
        {
            Debug.Log("it is STREET FLUSH");
            prizeAmount = (int)GoalsManager.DefaultPrizes.StraightFlush;
            GoalsManager.Instance.ShowWinCombinationBorder(GoalsManager.DefaultPrizes.StraightFlush);
            winningCombination = GoalsManager.Instance.WinCombinationDictionary[GoalsManager.WinCombination.STRAIGHT_FLUSH];
        }
        else if (IsPoker(cards))
        {
            Debug.Log("it is POKER");
            prizeAmount = (int)GoalsManager.DefaultPrizes.Poker;
            GoalsManager.Instance.ShowWinCombinationBorder(GoalsManager.DefaultPrizes.Poker);
            winningCombination = GoalsManager.Instance.WinCombinationDictionary[GoalsManager.WinCombination.POKER];
        }
        else if (IsFullHouse(cards))
        {
            Debug.Log("it is FULL HOUSE");
            prizeAmount = (int)GoalsManager.DefaultPrizes.FullHouse;
            GoalsManager.Instance.ShowWinCombinationBorder(GoalsManager.DefaultPrizes.FullHouse);
            winningCombination = GoalsManager.Instance.WinCombinationDictionary[GoalsManager.WinCombination.FULL_HOUSE];
        }
        else if (IsFlush(cards))
        {
            Debug.Log("it is FLUSH");
            prizeAmount = (int)GoalsManager.DefaultPrizes.Flush;
            GoalsManager.Instance.ShowWinCombinationBorder(GoalsManager.DefaultPrizes.Flush);
            winningCombination = GoalsManager.Instance.WinCombinationDictionary[GoalsManager.WinCombination.FLUSH];
        }
        else if (IsStraight(cards))
        {
            Debug.Log("it is STREET");
            prizeAmount = (int)GoalsManager.DefaultPrizes.Straight;
            GoalsManager.Instance.ShowWinCombinationBorder(GoalsManager.DefaultPrizes.Straight);
            winningCombination = GoalsManager.Instance.WinCombinationDictionary[GoalsManager.WinCombination.STRAIGHT];
        }
        else if (IsThreeOfAKind(cards))
        {
            Debug.Log("it is THREE OF A KIND");
            prizeAmount = (int)GoalsManager.DefaultPrizes.ThreeOfKind;
            GoalsManager.Instance.ShowWinCombinationBorder(GoalsManager.DefaultPrizes.ThreeOfKind);
            winningCombination = GoalsManager.Instance.WinCombinationDictionary[GoalsManager.WinCombination.THREE_OF_A_KIND];
        }
        else if (IsDoublePair(cards))
        {
            Debug.Log("it is DOUBLE PAIR");
            prizeAmount = (int)GoalsManager.DefaultPrizes.DoublePair;
            GoalsManager.Instance.ShowWinCombinationBorder(GoalsManager.DefaultPrizes.DoublePair);
            winningCombination = GoalsManager.Instance.WinCombinationDictionary[GoalsManager.WinCombination.DOUBLE_PAIR];
        }
        else if (IsHighPair(cards))
        {
            Debug.Log("it is HIGH PAIR");
            prizeAmount = (int)GoalsManager.DefaultPrizes.HighPair;
            GoalsManager.Instance.ShowWinCombinationBorder(GoalsManager.DefaultPrizes.HighPair);
            winningCombination = GoalsManager.Instance.WinCombinationDictionary[GoalsManager.WinCombination.HIGH_PAIR];
        }

        if(currentRound == CardDealer.DealRoundEnum.Second && prizeAmount > 0)
        {
            EventManager.Instance.GamblingReady.Invoke(prizeAmount);
            EventManager.Instance.WinningCombinationTextUpdate.Invoke(winningCombination, prizeAmount);
        }
    }

    private void AnalyzeCards(List<Card> cards)
    {
        foreach (Card element in cards)
        {
            if (element.SuitType == Card.Suit.Heart)
                heartTotal++;
            else if (element.SuitType == Card.Suit.Diamond)
                diamondTotal++;
            else if (element.SuitType == Card.Suit.Club)
                clubTotal++;
            else if (element.SuitType == Card.Suit.Spade)
                spadeTotal++;
            else
                jokerTotal++;

            if (CardValueCountDictionary.ContainsKey(element.CardValue))
            {
                CardValueCountDictionary[element.CardValue]++;
            }
            else
            {
                CardValueCountDictionary.Add(element.CardValue, 1);
            }
        }
    }

    private bool IsFiveOfAKind(List<Card> cards)
    {
        bool isThereFours = false;
        foreach (KeyValuePair<int, int> keyValue in CardValueCountDictionary)
        {
            if (keyValue.Value == 4)
            {
                isThereFours = true;
                break;
            }
        }

        if (isThereFours && jokerTotal > 0)
        {
            foreach (Card element in cards)
            {
                element.ParentCardSlot.LockCard();
            }

            return true;
        }

        return false;
    }

    private bool IsFlush(List<Card> cards)
    {
        if (heartTotal + jokerTotal == 5 || diamondTotal + jokerTotal == 5 || clubTotal + jokerTotal == 5 || spadeTotal + jokerTotal == 5)
        {
            foreach (Card element in cards)
            {
                element.ParentCardSlot.LockCard();
            }

            return true;
        }

        return false;
    }

    private bool IsPoker(List<Card> cards)
    {
        bool isThereFours = false;
        bool isThereThrees = false;
        int cardValue = 0;

        foreach (KeyValuePair<int, int> keyValue in CardValueCountDictionary)
        {
            if (keyValue.Value == 4)
            {
                cardValue = keyValue.Key;
                isThereFours = true;
                break;
            }

            if (keyValue.Value == 3)
            {
                cardValue = keyValue.Key;
                isThereThrees = true;
                break;
            }
        }

        if (isThereFours)
        {
            foreach (Card element in cards)
            {
                if (element.CardValue == cardValue)
                    element.ParentCardSlot.LockCard();
            }

            return true;
        }

        if (isThereThrees && jokerTotal > 0)
        {
            foreach (Card element in cards)
            {
                if (element.CardValue == cardValue || (element.CardValue == 0 && element.CardType == Card.CardTypeEnum.Joker))
                {
                    element.ParentCardSlot.LockCard();
                }
            }

            return true;
        }

        return false;
    }

    private bool IsStraight(List<Card> cards)
    {
        int firstCardValue = 0;

        // situation 1
        // 10, 11, 12, 13, 14

        // situation 2
        // 0, 10, 11, 12, 13

        if (cards[0].CardValue == 0)
        {
            firstCardValue = cards[1].CardValue - 1;
        }
        else
        {
            firstCardValue = cards[0].CardValue;
        }

        //if 5 consecutive values
        if (firstCardValue + 1 == cards[1].CardValue &&
            cards[1].CardValue + 1 == cards[2].CardValue &&
            cards[2].CardValue + 1 == cards[3].CardValue &&
            cards[3].CardValue + 1 == cards[4].CardValue)
        {
            cards[0].ParentCardSlot.LockCard();
            cards[1].ParentCardSlot.LockCard();
            cards[2].ParentCardSlot.LockCard();
            cards[3].ParentCardSlot.LockCard();
            cards[4].ParentCardSlot.LockCard();
            return true;
        }

        return false;
    }

    private bool IsRoyalFlush(List<Card> cards)
    {
        if (IsStraightFlush(cards))
        {
            if (cards[4].CardValue + 1 == 14 || cards[4].CardValue == 14)
            {
                return true;
            }
        }

        return false;
    }

    private bool IsStraightFlush(List<Card> cards)
    {
        return IsStraight(cards) && IsFlush(cards);
    }

    private bool IsFullHouse(List<Card> cards)
    {
        if (jokerTotal > 0)
        {
            if ((cards[4].CardValue == cards[3].CardValue && cards[4].CardValue == cards[2].CardValue) ||
            (cards[1].CardValue == cards[2].CardValue && cards[1].CardValue == cards[3].CardValue) ||
            (cards[1].CardValue == cards[2].CardValue && cards[3].CardValue == cards[4].CardValue))
            {
                cards[1].ParentCardSlot.LockCard();
                cards[2].ParentCardSlot.LockCard();
                cards[3].ParentCardSlot.LockCard();
                cards[4].ParentCardSlot.LockCard();

                foreach (Card element in cards)
                {
                    if (element.CardValue == 0 && element.CardType == Card.CardTypeEnum.Joker)
                    {
                        element.ParentCardSlot.LockCard();
                    }
                }

                return true;
            }
        }

        if ((cards[0].CardValue == cards[1].CardValue && cards[0].CardValue == cards[2].CardValue && cards[3].CardValue == cards[4].CardValue) ||
            (cards[0].CardValue == cards[1].CardValue && cards[2].CardValue == cards[3].CardValue && cards[2].CardValue == cards[4].CardValue))
        {
            cards[0].ParentCardSlot.LockCard();
            cards[1].ParentCardSlot.LockCard();
            cards[2].ParentCardSlot.LockCard();
            cards[3].ParentCardSlot.LockCard();
            cards[4].ParentCardSlot.LockCard();
            return true;
        }

        return false;
    }

    private bool IsThreeOfAKind(List<Card> cards)
    {
        bool isThereThrees = false;
        int cardValue = 0;

        foreach (KeyValuePair<int, int> keyValue in CardValueCountDictionary)
        {
            if (keyValue.Value == 3)
            {
                cardValue = keyValue.Key;
                isThereThrees = true;
                break;
            }
        }

        if (isThereThrees)
        {
            foreach (Card element in cards)
            {
                if (element.CardValue == cardValue)
                    element.ParentCardSlot.LockCard();
            }
            return true;
        }

        if (jokerTotal > 0)
        {
            foreach (KeyValuePair<int, int> keyValue in CardValueCountDictionary)
            {
                if (keyValue.Value == 2)
                {
                    cardValue = keyValue.Key;

                    foreach (Card element in cards)
                    {
                        if (element.CardValue == cardValue || (element.CardValue == 0 && element.CardType == Card.CardTypeEnum.Joker))
                        {
                            element.ParentCardSlot.LockCard();
                        }
                    }

                    return true;
                }
            }
        }

        return false;
    }

    private bool IsDoublePair(List<Card> cards)
    {
        bool pairOne = false;
        bool pairTwo = false;
        int cardValue_1 = 0;
        int cardValue_2 = 0;

        foreach (KeyValuePair<int, int> keyValue in CardValueCountDictionary)
        {
            if (keyValue.Value == 2 && !pairOne)
            {
                cardValue_1 = keyValue.Key;
                pairOne = true;
                continue;
            }

            if (keyValue.Value == 2 && !pairTwo)
            {
                cardValue_2 = keyValue.Key;
                pairTwo = true;
                continue;
            }
        }

        if (pairOne && pairTwo)
        {
            foreach (Card element in cards)
            {
                if (element.CardValue == cardValue_1 || element.CardValue == cardValue_2)
                {
                    element.ParentCardSlot.LockCard();
                }
            }

            return true;
        }

        return false;
    }

    private bool IsHighPair(List<Card> cards)
    {
        int cardValue = 0;

        foreach (KeyValuePair<int, int> keyValue in CardValueCountDictionary)
        {
            if ((keyValue.Key >= 11 && keyValue.Value > 1 && jokerTotal == 0) ||
                (keyValue.Key >= 11 && jokerTotal > 0))
            {
                cardValue = keyValue.Key;
            }
        }

        if (jokerTotal > 0)
        {
            if ((cards[4].CardValue >= 11) ||
                (cards[3].CardValue >= 11) ||
                (cards[2].CardValue >= 11) ||
                (cards[1].CardValue >= 11))
            {
                foreach (Card element in cards)
                {
                    if (element.CardValue == cardValue || (element.CardValue == 0 && element.CardType == Card.CardTypeEnum.Joker))
                    {
                        element.ParentCardSlot.LockCard();
                    }
                }

                return true;
            }
        }
        else
        {
            if ((cards[4].CardValue == cards[3].CardValue && cards[4].CardValue >= 11) ||
                (cards[3].CardValue == cards[2].CardValue && cards[3].CardValue >= 11) ||
                (cards[2].CardValue == cards[1].CardValue && cards[2].CardValue >= 11) ||
                (cards[1].CardValue == cards[0].CardValue && cards[1].CardValue >= 11))
            {
                foreach (Card element in cards)
                {
                    if (element.CardValue == cardValue || (element.CardValue == 0 && element.CardType == Card.CardTypeEnum.Joker))
                    {
                        element.ParentCardSlot.LockCard();
                    }
                }
                return true;
            }
        }

        return false;
    }
}
