using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static EventManager _instance;
    public CustomEvent<CardDealer.DealRoundEnum> DealRoundChange = new CustomEvent<CardDealer.DealRoundEnum>();
    public CustomEvent<List<Card>, CardDealer.DealRoundEnum> EvaluateHand = new CustomEvent<List<Card>, CardDealer.DealRoundEnum>();
    public CustomEvent<int> GamblingReady = new CustomEvent<int>();
    public CustomEvent NewRoundReady = new CustomEvent();
    public CustomEvent GamblingOver = new CustomEvent();
    public CustomEvent<string, int> WinningCombinationTextUpdate = new CustomEvent<string, int>();

    public static EventManager Instance
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
}
