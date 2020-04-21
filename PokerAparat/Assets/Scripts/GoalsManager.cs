using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalsManager : MonoBehaviour
{
    public enum DefaultPrizes { HighPair = 1, DoublePair = 2, ThreeOfKind = 3, Straight = 5, Flush = 7, FullHouse = 10, Poker = 40, StraightFlush = 100, RoyalFlush = 500, FiveOfKind = 1100 }
    private static GoalsManager _instance;
    public Text FiveOfAKindPrize;
    public Text PokerPrize;
    public Text RoyalFlushPrize;
    public Text StreetFlushPrize;
    public Text ThreeOfAKindPrize;
    public Text HighPairPrize;
    public Text FullHousePrize;
    public Text FlushPrize;
    public Text DoublePairPrize;
    public Text StreetPrize;

    public static GoalsManager Instance
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

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RecalculatePrizes(int bet)
    {
        HighPairPrize.text = string.Format("{0}", (int)DefaultPrizes.HighPair * bet);
        DoublePairPrize.text = string.Format("{0}", (int)DefaultPrizes.DoublePair * bet);
        ThreeOfAKindPrize.text = string.Format("{0}", (int)DefaultPrizes.ThreeOfKind * bet);
        StreetPrize.text = string.Format("{0}", (int)DefaultPrizes.Straight * bet);
        FlushPrize.text = string.Format("{0}", (int)DefaultPrizes.Flush * bet);
        FullHousePrize.text = string.Format("{0}", (int)DefaultPrizes.FullHouse * bet);
        PokerPrize.text = string.Format("{0}", (int)DefaultPrizes.Poker * bet);
        StreetFlushPrize.text = string.Format("{0}", (int)DefaultPrizes.StraightFlush * bet);
        RoyalFlushPrize.text = string.Format("{0}", (int)DefaultPrizes.RoyalFlush * bet);
        FiveOfAKindPrize.text = string.Format("{0}", (int)DefaultPrizes.FiveOfKind * bet);
    }
}
