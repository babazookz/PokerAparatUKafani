using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandEvaluator : MonoBehaviour
{
    private static HandEvaluator _instance;
    private int heartTotal;
    private int diamondTotal;
    private int clubTotal;
    private int spadeTotal;

    public static HandEvaluator Instance
    {
        get
        {
            return _instance;
        }
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
        heartTotal = 0;
        diamondTotal = 0;
        clubTotal = 0;
        spadeTotal = 0;
    }

    private void getNumberOfSuit(List<Card> cards)
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
        }
    }
}
