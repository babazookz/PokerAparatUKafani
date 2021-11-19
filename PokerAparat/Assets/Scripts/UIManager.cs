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
    public Button IncreaseBetButton, DecreaseBetButton;
    private bool _betIncreaseButtonHolding = false;
    private bool _betDecreaseButtonHolding = false;
    private float _betButtonHoldingTimeCurrentSequence = 0;
    private float _totalBetButtonHoldingTime = 0;

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
        IncreaseBetButton.onClick.AddListener(OnIncreaseBetClick);
        DecreaseBetButton.onClick.AddListener(OnDecreaseBetClick);
        EventManager.Instance.GamblingReady.AddListener(OnGamblingReadyHandler);
        EventManager.Instance.NewRoundReady.AddListener(OnNewRoundReadyHandler);
        EventManager.Instance.WinningCombinationTextUpdate.AddListener(PrepareWinningCombinationText);
        EventManager.Instance.GamblingOver.AddListener(OnGamblingOverHandler);

        LowCardButton.onClick.AddListener(DrawLowCard);
        HighCardButton.onClick.AddListener(DrawHighCard);
        HalfButton.onClick.AddListener(HalfThePrizeAction);
    }

    private void OnDestroy()
    {
        EventManager.Instance.GamblingReady.RemoveListener(OnGamblingReadyHandler);
        EventManager.Instance.NewRoundReady.RemoveListener(OnNewRoundReadyHandler);
        EventManager.Instance.WinningCombinationTextUpdate.RemoveListener(PrepareWinningCombinationText);
        EventManager.Instance.GamblingOver.RemoveListener(OnGamblingOverHandler);
    }

    void Update()
    {
        if (_betIncreaseButtonHolding || _betDecreaseButtonHolding)
        {
            _betButtonHoldingTimeCurrentSequence += Time.deltaTime;
            _totalBetButtonHoldingTime += Time.deltaTime;

            if (_totalBetButtonHoldingTime >= 4f)
            {
                if (_betButtonHoldingTimeCurrentSequence >= 1)
                {
                    if (_betIncreaseButtonHolding) BetManager.Instance.IncreaseBet(10);
                    else BetManager.Instance.DecreaseBet(10);
                    _betButtonHoldingTimeCurrentSequence = 0;
                }
            }
            else if (_totalBetButtonHoldingTime >= 2.5f)
            {
                if (_betButtonHoldingTimeCurrentSequence >= 1)
                {
                    if (_betIncreaseButtonHolding) BetManager.Instance.IncreaseBet(5);
                    else BetManager.Instance.DecreaseBet(5);
                    _betButtonHoldingTimeCurrentSequence = 0;
                }
            }
            else
            {
                if (_betButtonHoldingTimeCurrentSequence >= 1)
                {
                    if (_betIncreaseButtonHolding) BetManager.Instance.IncreaseBet(1);
                    else BetManager.Instance.DecreaseBet(1);
                    _betButtonHoldingTimeCurrentSequence = 0;
                }
            }
        }
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
        CardDealer.Instance.DealRound = CardDealer.DealRoundEnum.Zero;
        IncreaseBetButton.gameObject.SetActive(false);
        DecreaseBetButton.gameObject.SetActive(false);
    }

    private void ToggleGamblingButton(bool active)
    {
        GambleButton.gameObject.SetActive(active);
    }

    private void OnGamblingOverHandler()
    {
        DoubleOrNothingButtonPanel.gameObject.SetActive(false);
        ToggleDrawCardsButton(true);
        IncreaseBetButton.gameObject.SetActive(true);
        DecreaseBetButton.gameObject.SetActive(true);
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
        // nazvati i promijeniti malo ovu ispod metodu
        DoubleOrNothingManager.Instance.DoubleOrNothingFinishAction();
        CardSlotManager.Instance.DealCards();
    }

    void PrepareWinningCombinationText(string winCombination, int winAmount)
    {
        WinCombinationDescriptionText.text = winCombination;
        WinAmountText.text = winAmount.ToString();
    }

    public void UpdateDoubleOrNothingAmount(int winAmount)
    {
        _currentWinAmount = winAmount;
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
        int newAmount = DoubleOrNothingManager.Instance.HalfThePrize();
        _currentWinAmount = newAmount;
        UpdateDoubleOrNothingAmount(_currentWinAmount);
    }

    public void OnIncreaseBetHandler()
    {
        _betIncreaseButtonHolding = true;
        _betDecreaseButtonHolding = false;
            
    }

    public void OnDecreaseBetHandler()
    {
        _betIncreaseButtonHolding = false;
        _betDecreaseButtonHolding = true;
    }

    public void OnIncreaseBetClick()
    {
        BetManager.Instance.IncreaseBet(1);
    }

    public void OnDecreaseBetClick()
    {
        BetManager.Instance.DecreaseBet(1);
    }

    public void OnBetButtonsPointerUp()
    {
        _betIncreaseButtonHolding = false;
        _betDecreaseButtonHolding = false;
        _totalBetButtonHoldingTime = 0;
        _betButtonHoldingTimeCurrentSequence = 0;
    }
}
