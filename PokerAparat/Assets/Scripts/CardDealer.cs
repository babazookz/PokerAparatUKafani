using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDealer : MonoBehaviour
{
    private static CardDealer _instance;
    private Transform _transform;
    private Object[] cardResources;
    public Dictionary<Card.CardTypeEnum, Card> CardDictionary = new Dictionary<Card.CardTypeEnum, Card>();
    public enum DealRoundEnum { First, Second, Zero }
    public DealRoundEnum DealRound;

    public static CardDealer Instance
    {
        get
        {
            return _instance;
        }
    }

    public Transform ObjectTransform
    {
        get
        {
            return _transform;
        }
    }

    private void Awake()
    {
        DealRound = DealRoundEnum.Zero;
        _transform = transform;
        _instance = this;
        CardDictionary.Clear();
    }

    void Start()
    {
        LoadAllCardResources();
    }

    private void LoadAllCardResources()
    {
        cardResources = Resources.LoadAll("Cards") as Object[];

        if (cardResources == null)
            Debug.Log("Cards not found!");

        foreach (Object go in cardResources)
        {
            GameObject _go = GameObject.Instantiate((GameObject)go, _transform) as GameObject;
            _go.SetActive(false);
            _go.name = go.name;
            Card _card = _go.GetComponent<Card>();

            if (_card == null)
            {
                Debug.Log("Card Component not found! -> " + _go.name);
                continue;
            }

            CardDictionary.Add(_card.CardType, _card);
        }
    }
}
