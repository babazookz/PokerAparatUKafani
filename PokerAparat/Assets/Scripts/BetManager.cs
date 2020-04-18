using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BetManager : MonoBehaviour
{
    private static BetManager _instance;
    private int _currentBet = 1;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        this.OnMyCurrentBetVariableChange += MyCurrentBetVariableChangeHandler;
        GoalsManager.Instance.RecalculatePrizes(MyCurrentBet);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void MyCurrentBetVariableChangeHandler(int newVal)
    {
        Debug.Log("MyCurrentBet is now - " + MyCurrentBet.ToString());
        GoalsManager.Instance.RecalculatePrizes(MyCurrentBet);
    }
}
