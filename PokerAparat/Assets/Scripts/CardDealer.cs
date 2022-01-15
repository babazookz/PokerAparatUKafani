using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDealer : MonoBehaviour
{
    private static CardDealer _instance;
    private Transform _transform;
    private Object[] cardResources;
    private List<GameObject> cardGameObjectResources;
    public Dictionary<Card.CardTypeEnum, Card> CardDictionary = new Dictionary<Card.CardTypeEnum, Card>();
    public enum DealRoundEnum { First, Second, Zero }
    public Dictionary<string, int> CardValueDictionary = new Dictionary<string, int>();
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

        CardValueDictionary.Add("Club_2", 2);
        CardValueDictionary.Add("Club_3", 3);
        CardValueDictionary.Add("Club_4", 4);
        CardValueDictionary.Add("Club_5", 5);
        CardValueDictionary.Add("Club_6", 6);
        CardValueDictionary.Add("Club_7", 7);
        CardValueDictionary.Add("Club_8", 8);
        CardValueDictionary.Add("Club_9", 9);
        CardValueDictionary.Add("Club_10", 10);
        CardValueDictionary.Add("Club_J", 11);
        CardValueDictionary.Add("Club_Q", 12);
        CardValueDictionary.Add("Club_K", 13);
        CardValueDictionary.Add("Club_A", 14);

        CardValueDictionary.Add("Diamond_2", 2);
        CardValueDictionary.Add("Diamond_3", 3);
        CardValueDictionary.Add("Diamond_4", 4);
        CardValueDictionary.Add("Diamond_5", 5);
        CardValueDictionary.Add("Diamond_6", 6);
        CardValueDictionary.Add("Diamond_7", 7);
        CardValueDictionary.Add("Diamond_8", 8);
        CardValueDictionary.Add("Diamond_9", 9);
        CardValueDictionary.Add("Diamond_10", 10);
        CardValueDictionary.Add("Diamond_J", 11);
        CardValueDictionary.Add("Diamond_Q", 12);
        CardValueDictionary.Add("Diamond_K", 13);
        CardValueDictionary.Add("Diamond_A", 14);

        CardValueDictionary.Add("Spade_2", 2);
        CardValueDictionary.Add("Spade_3", 3);
        CardValueDictionary.Add("Spade_4", 4);
        CardValueDictionary.Add("Spade_5", 5);
        CardValueDictionary.Add("Spade_6", 6);
        CardValueDictionary.Add("Spade_7", 7);
        CardValueDictionary.Add("Spade_8", 8);
        CardValueDictionary.Add("Spade_9", 9);
        CardValueDictionary.Add("Spade_10", 10);
        CardValueDictionary.Add("Spade_J", 11);
        CardValueDictionary.Add("Spade_Q", 12);
        CardValueDictionary.Add("Spade_K", 13);
        CardValueDictionary.Add("Spade_A", 14);

        CardValueDictionary.Add("Heart_2", 2);
        CardValueDictionary.Add("Heart_3", 3);
        CardValueDictionary.Add("Heart_4", 4);
        CardValueDictionary.Add("Heart_5", 5);
        CardValueDictionary.Add("Heart_6", 6);
        CardValueDictionary.Add("Heart_7", 7);
        CardValueDictionary.Add("Heart_8", 8);
        CardValueDictionary.Add("Heart_9", 9);
        CardValueDictionary.Add("Heart_10", 10);
        CardValueDictionary.Add("Heart_J", 11);
        CardValueDictionary.Add("Heart_Q", 12);
        CardValueDictionary.Add("Heart_K", 13);
        CardValueDictionary.Add("Heart_A", 14);

    }

    void Start()
    {
        LoadAllCardResources();
        PopulateDictionary();
    }

    private void LoadAllCardResources()
    {
        cardResources = Resources.LoadAll("Cards") as Object[];

        if (cardResources == null)
            Debug.Log("Cards not found!");

        cardGameObjectResources = new List<GameObject>();

        foreach (Object go in cardResources)
        {
            if(go.name != "Card_Back")
            {
                GameObject _go = GameObject.Instantiate((GameObject)go, _transform) as GameObject;
                _go.SetActive(false);
                _go.name = go.name;

                cardGameObjectResources.Add(_go);
            }
        }
    }

    private void PopulateDictionary()
    {
        foreach (GameObject go in cardGameObjectResources)
        {
            Card _card = go.GetComponent<Card>();

            if (_card == null)
            {
                Debug.Log("Card Component not found! -> " + go.name);
                continue;
            }

            //_card.PopulateCardType();
            if (CardValueDictionary.ContainsKey(_card.name))
            {
                _card.CardValue = CardValueDictionary[_card.name];
            }
            else
            {
                _card.CardValue = 0;
            }
            CardDictionary.Add(_card.CardType, _card);
            //go.SetActive(false);
        }
    }
}
