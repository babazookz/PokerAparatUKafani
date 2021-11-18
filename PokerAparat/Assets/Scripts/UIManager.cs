using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Color SelectedCardBackgroundColor;
    private static UIManager _instance;
    public Transform DoubleOrNothingPanel;
    public Transform DoubleOrNothingTopPanel;
    public Transform DoubleOrNothingButtonPanel;
    public Transform MainSlotPanel;
    public Transform GoalsTopPanel;
    public Font AppFont;
    public Button GambleButton;
    public Button DrawCardsButton;
    public Text WinCombinationDescriptionText;
    public Text WinAmountText;
    public Text HighCardsExplanationText;
    public Button LowCardButton, HighCardButton, HalfButton;
    private int _currentWinAmount;

    public static UIManager Instance
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
        OnNewRoundReadyHandler();
        GambleButton.onClick.AddListener(PrepareGambleScreen);
        DrawCardsButton.onClick.AddListener(DealCards);
        EventManager.Instance.DealRoundChange.AddListener(OnDealRoundChangeHandler);
        EventManager.Instance.GamblingReady.AddListener(OnGamblingReadyHandler);
        EventManager.Instance.NewRoundReady.AddListener(OnNewRoundReadyHandler);
        EventManager.Instance.WinningCombinationTextUpdate.AddListener(PrepareWinningCombinationText);

        LowCardButton.onClick.AddListener(DrawLowCard);
        HighCardButton.onClick.AddListener(DrawHighCard);
        HalfButton.onClick.AddListener(HalfThePrizeAction);
    }

    private void OnDestroy()
    {
        EventManager.Instance.DealRoundChange.RemoveListener(OnDealRoundChangeHandler);
        EventManager.Instance.GamblingReady.RemoveListener(OnGamblingReadyHandler);
        EventManager.Instance.NewRoundReady.RemoveListener(OnNewRoundReadyHandler);
        EventManager.Instance.WinningCombinationTextUpdate.RemoveListener(PrepareWinningCombinationText);
    }

    private void OnDealRoundChangeHandler(CardDealer.DealRoundEnum newRound)
    {
        
    }

    private void PrepareGambleScreen()
    {
        ToggleMainSlotScreen(false);
        ToggleDoubleOrNothingScreen(true);
        ToggleGamblingButton(false);
        ToggleHighCardsExplanation(true);
        DoubleOrNothingManager.Instance.PrepareDoubleOrNothingData(_currentWinAmount);
    }

    private void OnGamblingReadyHandler(int currentPrizeAmount)
    {
        // show gambling button and top info panel
        ToggleGoalsTopPanel(false);
        ToggleWinningCombinationTopPanel(true);
        ToggleDrawCardsButton(false);
        ToggleGamblingButton(true);
        _currentWinAmount = currentPrizeAmount;
    }

    private void ToggleGamblingButton(bool active)
    {
        GambleButton.gameObject.SetActive(active);
    }

    private void OnNewRoundReadyHandler()
    {
        ToggleMainSlotScreen(true);
        ToggleDoubleOrNothingScreen(false);
        ToggleGoalsTopPanel(true);
        ToggleWinningCombinationTopPanel(false);
        ToggleGamblingButton(false);
        ToggleDrawCardsButton(true);
        ToggleHighCardsExplanation(false);
    }

    public void ToggleDoubleOrNothingScreen(bool active)
    {
        DoubleOrNothingPanel.gameObject.SetActive(active);
        DoubleOrNothingButtonPanel.gameObject.SetActive(active);
    }

    public void ToggleWinningCombinationTopPanel(bool active)
    {
        DoubleOrNothingTopPanel.gameObject.SetActive(active);
    }

    public void ToggleMainSlotScreen(bool active)
    {
        MainSlotPanel.gameObject.SetActive(active);   
    }

    public void ToggleGoalsTopPanel(bool active)
    {
        GoalsTopPanel.gameObject.SetActive(active);
    }

    void ToggleDrawCardsButton(bool active)
    {
        DrawCardsButton.gameObject.SetActive(active);
    }

    public void DealCards()
    {
        CardSlotManager.Instance.DealCards();
    }

    void PrepareWinningCombinationText(string winCombination, int winAmount)
    {
        WinCombinationDescriptionText.text = winCombination;
        WinAmountText.text = winAmount.ToString();
    }

    public void UpdateDoubleOrNothingAmount(int winAmount)
    {
        WinAmountText.text = winAmount.ToString();
    }

    public void ToggleHighCardsExplanation(bool active)
    {
        HighCardsExplanationText.gameObject.SetActive(active);
    }

    void DrawLowCard()
    {
        DoubleOrNothingManager.Instance.DrawNewCard(true);
    }

    void DrawHighCard()
    {
        DoubleOrNothingManager.Instance.DrawNewCard(false);
    }

    void HalfThePrizeAction()
    {

    }
}
