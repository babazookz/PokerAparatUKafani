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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EvaluateHand(List<Card> cards)
    {
        CardValueCountDictionary.Clear();
        heartTotal = 0;
        diamondTotal = 0;
        clubTotal = 0;
        spadeTotal = 0;
        jokerTotal = 0;

        // sort by card value
        cards = cards.OrderBy(ob => ob.CardValue).ToList();

        AnalyzeCards(cards);

        if (IsFiveOfAKind())
        {
            Debug.Log("it is FIVE OF A KIND");
        }
        else if (IsRoyalFlush(cards))
        {
            Debug.Log("it is ROYAL FLUSH");
        }
        else if (IsStraightFlush(cards))
        {
            Debug.Log("it is STREET FLUSH");
        }
        else if (IsPoker())
        {
            Debug.Log("it is FOUR OF A KIND");
        }
        else if (IsFullHouse(cards))
        {
            Debug.Log("it is FULL HOUSE");
        }
        else if (IsFlush())
        {
            Debug.Log("it is FLUSH");
        }
        else if (IsStraight(cards))
        {
            Debug.Log("it is STREET");
        }
        else if (IsThreeOfAKind())
        {
            Debug.Log("it is THREE OF A KIND");
        }
        else if (IsDoublePair())
        {
            Debug.Log("it is DOUBLE PAIR");
        }
        else if (IsHighPair(cards))
        {
            Debug.Log("it is HIGH PAIR");
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

    private bool IsFiveOfAKind()
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
            return true;
        }

        return false;
    }

    private bool IsFlush()
    {
        if (heartTotal + jokerTotal == 5 || diamondTotal + jokerTotal == 5 || clubTotal + jokerTotal == 5 || spadeTotal + jokerTotal == 5)
        {
            return true;
        }

        return false;
    }

    private bool IsPoker()
    {
        bool isThereFours = false;
        bool isThereThrees = false;

        foreach (KeyValuePair<int, int> keyValue in CardValueCountDictionary)
        {
            if (keyValue.Value == 4)
            {
                isThereFours = true;
                break;
            }

            if (keyValue.Value == 3)
            {
                isThereThrees = true;
                break;
            }
        }

        if (isThereFours)
        {
            return true;
        }

        if (isThereThrees && jokerTotal > 0)
        {
            return true;
        }

        return false;
    }

    private bool IsStraight(List<Card> cards)
    {
        // simulate Joker card
        int firstCardValue = 0;

        if (cards[0].CardValue == 0)
        {
            firstCardValue = cards[1].CardValue - 1;
        }

        //if 5 consecutive values
        if (firstCardValue + 1 == cards[1].CardValue &&
            cards[1].CardValue + 1 == cards[2].CardValue &&
            cards[2].CardValue + 1 == cards[3].CardValue &&
            cards[3].CardValue + 1 == cards[4].CardValue)
        {
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
        return IsStraight(cards) && IsFlush();
    }

    private bool IsFullHouse(List<Card> cards)
    {
        if (jokerTotal > 0)
        {
            if ((cards[4].CardValue == cards[3].CardValue && cards[4].CardValue == cards[2].CardValue) ||
            (cards[1].CardValue == cards[2].CardValue && cards[1].CardValue == cards[3].CardValue))
            {
                return true;
            }
        }

        if ((cards[0].CardValue == cards[1].CardValue && cards[0].CardValue == cards[2].CardValue && cards[3].CardValue == cards[4].CardValue) ||
            (cards[0].CardValue == cards[1].CardValue && cards[2].CardValue == cards[3].CardValue && cards[2].CardValue == cards[4].CardValue))
        {
            return true;
        }

        return false;
    }

    private bool IsThreeOfAKind()
    {
        bool isThereThrees = false;

        foreach (KeyValuePair<int, int> keyValue in CardValueCountDictionary)
        {
            if (keyValue.Value == 3)
            {
                isThereThrees = true;
                break;
            }
        }

        if (isThereThrees)
        {
            return true;
        }

        if (jokerTotal > 0)
        {
            foreach (KeyValuePair<int, int> keyValue in CardValueCountDictionary)
            {
                if (keyValue.Value == 2)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool IsDoublePair()
    {
        bool pairOne = false;
        bool pairTwo = false;

        foreach (KeyValuePair<int, int> keyValue in CardValueCountDictionary)
        {
            if (keyValue.Value == 2 && !pairOne)
            {
                pairOne = true;
                continue;
            }

            if (keyValue.Value == 2 && !pairTwo)
            {
                pairTwo = true;
                continue;
            }
        }

        if (pairOne && pairTwo)
        {
            return true;
        }

        return false;
    }

    private bool IsHighPair(List<Card> cards)
    {
        if (jokerTotal > 0)
        {
            if ((cards[4].CardValue >= 11) ||
                (cards[3].CardValue >= 11) ||
                (cards[2].CardValue >= 11) ||
                (cards[1].CardValue >= 11))
            {
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
                return true;
            }
        }

        return false;
    }
}
