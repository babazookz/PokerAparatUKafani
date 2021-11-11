using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalsManager : MonoBehaviour
{
    public enum DefaultPrizes { Nothing = 0, HighPair = 1, DoublePair = 2, ThreeOfKind = 3, Straight = 5, Flush = 7, FullHouse = 10, Poker = 40, StraightFlush = 100, RoyalFlush = 500, FiveOfKind = 1100 }
    private static GoalsManager _instance;

    [Header("Goal Prizes")]
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

    [Header("Goal Borders")]
    public Image FiveOfAKindBorder;
    public Image PokerBorder;
    public Image RoyalFlushBorder;
    public Image StreetFlushBorder;
    public Image ThreeOfAKindBorder;
    public Image HighPairBorder;
    public Image FullHouseBorder;
    public Image FlushBorder;
    public Image DoublePairBorder;
    public Image StreetBorder;

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

    public void ShowWinCombinationBorder (DefaultPrizes prizeEnum)
    {
        switch(prizeEnum)
        {
            case DefaultPrizes.HighPair:
                HighPairBorder.gameObject.SetActive(true);
                break;
            case DefaultPrizes.DoublePair:
                DoublePairBorder.gameObject.SetActive(true);
                break;
            case DefaultPrizes.ThreeOfKind:
                ThreeOfAKindBorder.gameObject.SetActive(true);
                break;
            case DefaultPrizes.Straight:
                StreetBorder.gameObject.SetActive(true);
                break;
            case DefaultPrizes.Flush:
                FlushBorder.gameObject.SetActive(true);
                break;
            case DefaultPrizes.FullHouse:
                FullHouseBorder.gameObject.SetActive(true);
                break;
            case DefaultPrizes.Poker:
                PokerBorder.gameObject.SetActive(true);
                break;
            case DefaultPrizes.StraightFlush:
                StreetFlushBorder.gameObject.SetActive(true);
                break;
            case DefaultPrizes.RoyalFlush:
                RoyalFlushBorder.gameObject.SetActive(true);
                break;
            case DefaultPrizes.FiveOfKind:
                FiveOfAKindBorder.gameObject.SetActive(true);
                break;
            default:
                FiveOfAKindBorder.gameObject.SetActive(false);
                PokerBorder.gameObject.SetActive(false);
                RoyalFlushBorder.gameObject.SetActive(false);
                StreetFlushBorder.gameObject.SetActive(false);
                ThreeOfAKindBorder.gameObject.SetActive(false);
                HighPairBorder.gameObject.SetActive(false);
                FullHouseBorder.gameObject.SetActive(false);
                FlushBorder.gameObject.SetActive(false);
                DoublePairBorder.gameObject.SetActive(false);
                StreetBorder.gameObject.SetActive(false);
                break;

        }
    }
}
