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
        EventManager.Instance.NewRoundReady.AddListener(OnGamblingReadyHandler);
        EventManager.Instance.WinningCombinationTextUpdate.AddListener(PrepareWinningCombinationText);
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
    }

    private void OnGamblingReadyHandler()
    {
        // show gambling button and top info panel
        ToggleGoalsTopPanel(false);
        ToggleWinningCombinationTopPanel(true);
        ToggleDrawCardsButton(false);
        ToggleGamblingButton(true);
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
}
