using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlotManager : MonoBehaviour
{
    private static CardSlotManager _instance;
    public Transform CardSlotParent;
    private List<CardSlot> _cardSlots;

    public static CardSlotManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        _cardSlots = new List<CardSlot>();
        PrepareCardSlots();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void PrepareCardSlots()
    {
        if (CardSlotParent == null)
            return;

        Transform[] cardSlots = CardSlotParent.GetComponentsInChildren<Transform>();

        if (cardSlots == null)
            return;

        foreach (Transform slot in cardSlots)
        {
            CardSlot cs = slot.gameObject.GetComponent<CardSlot>();
            _cardSlots.Add(cs);
        }
    }
}
