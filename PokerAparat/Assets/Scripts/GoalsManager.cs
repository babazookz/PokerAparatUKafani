using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
        DOTween.Init();
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

    private void SetImageColorAlpha(Image img)
    {
        Color colorTmp = img.color;
        colorTmp.a = 1f;
        img.color = colorTmp;
    }

    private void ApplyFadeTweenToImage(Image img)
    {
        img.DOFade(0.25f, 1f).SetDelay(0.5f).SetLoops(-1, LoopType.Yoyo).Play();
    }

    public void ShowWinCombinationBorder (DefaultPrizes prizeEnum)
    {
        switch(prizeEnum)
        {
            case DefaultPrizes.HighPair:
                SetImageColorAlpha(HighPairBorder);
                HighPairBorder.gameObject.SetActive(true);
                ApplyFadeTweenToImage(HighPairBorder);
                break;
            case DefaultPrizes.DoublePair:
                SetImageColorAlpha(DoublePairBorder);
                DoublePairBorder.gameObject.SetActive(true);
                ApplyFadeTweenToImage(DoublePairBorder);
                break;
            case DefaultPrizes.ThreeOfKind:
                SetImageColorAlpha(ThreeOfAKindBorder);
                ThreeOfAKindBorder.gameObject.SetActive(true);
                ApplyFadeTweenToImage(ThreeOfAKindBorder);
                break;
            case DefaultPrizes.Straight:
                SetImageColorAlpha(StreetBorder);
                StreetBorder.gameObject.SetActive(true);
                ApplyFadeTweenToImage(StreetBorder);
                break;
            case DefaultPrizes.Flush:
                SetImageColorAlpha(FlushBorder);
                FlushBorder.gameObject.SetActive(true);
                ApplyFadeTweenToImage(FlushBorder);
                break;
            case DefaultPrizes.FullHouse:
                SetImageColorAlpha(FullHouseBorder);
                FullHouseBorder.gameObject.SetActive(true);
                ApplyFadeTweenToImage(FullHouseBorder);
                break;
            case DefaultPrizes.Poker:
                SetImageColorAlpha(PokerBorder);
                PokerBorder.gameObject.SetActive(true);
                ApplyFadeTweenToImage(PokerBorder);
                break;
            case DefaultPrizes.StraightFlush:
                SetImageColorAlpha(StreetFlushBorder);
                StreetFlushBorder.gameObject.SetActive(true);
                ApplyFadeTweenToImage(StreetFlushBorder);
                break;
            case DefaultPrizes.RoyalFlush:
                SetImageColorAlpha(RoyalFlushBorder);
                RoyalFlushBorder.gameObject.SetActive(true);
                ApplyFadeTweenToImage(RoyalFlushBorder);
                break;
            case DefaultPrizes.FiveOfKind:
                SetImageColorAlpha(FiveOfAKindBorder);
                FiveOfAKindBorder.gameObject.SetActive(true);
                ApplyFadeTweenToImage(FiveOfAKindBorder);
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

                FiveOfAKindBorder.DOKill();
                PokerBorder.DOKill();
                RoyalFlushBorder.DOKill();
                StreetFlushBorder.DOKill();
                ThreeOfAKindBorder.DOKill();
                HighPairBorder.DOKill();
                FullHouseBorder.DOKill();
                FlushBorder.DOKill();
                DoublePairBorder.DOKill();
                StreetBorder.DOKill();

                break;

        }
    }
}
