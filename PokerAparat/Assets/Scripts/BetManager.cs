using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BetManager : MonoBehaviour
{
    private static BetManager _instance;
    private int _currentBet = 1;
    public Text BetText;

    public int MyCurrentBet
    {
        get { return _currentBet; }
        set
        {
            if (_currentBet == value) return;
            _currentBet = value;

            if (OnMyCurrentBetVariableChange != null)
                OnMyCurrentBetVariableChange(_currentBet);
        }
    }

    public static BetManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public delegate void OnMyCurrentBetVariableChangeDelegate(int newVal);
    public event OnMyCurrentBetVariableChangeDelegate OnMyCurrentBetVariableChange;

    private void Awake()
    {
        _instance = this;
        MyCurrentBet = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.OnMyCurrentBetVariableChange += MyCurrentBetVariableChangeHandler;
        MyCurrentBet = PlayerPrefs.GetInt("CurrentBet", 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void MyCurrentBetVariableChangeHandler(int newVal)
    {
        Debug.Log("MyCurrentBet is now - " + MyCurrentBet.ToString());
        GoalsManager.Instance.RecalculatePrizes(MyCurrentBet);

        BetText.text = MyCurrentBet.ToString("# ###");
        PlayerPrefs.SetInt("CurrentBet", MyCurrentBet);
    }

    public void IncreaseBet(int amount)
    {
        MyCurrentBet += amount;
    }

    public void DecreaseBet(int amount)
    {
        MyCurrentBet -= amount;
    }
}
