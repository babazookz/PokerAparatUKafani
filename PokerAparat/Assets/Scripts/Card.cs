using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public enum CardTypeEnum
    {
        Club_2, Club_3, Club_4, Club_5, Club_6, Club_7, Club_8, Club_9, Club_10, Club_J, Club_Q, Club_K, Club_A,
        Diamond_2, Diamond_3, Diamond_4, Diamond_5, Diamond_6, Diamond_7, Diamond_8, Diamond_9, Diamond_10, Diamond_J, Diamond_Q, Diamond_K, Diamond_A,
        Spade_2, Spade_3, Spade_4, Spade_5, Spade_6, Spade_7, Spade_8, Spade_9, Spade_10, Spade_J, Spade_Q, Spade_K, Spade_A,
        Heart_2, Heart_3, Heart_4, Heart_5, Heart_6, Heart_7, Heart_8, Heart_9, Heart_10, Heart_J, Heart_Q, Heart_K, Heart_A,
        Joker
    }

    public enum Suit { Spade, Heart, Diamond, Club, Joker }
    public CardTypeEnum CardType;
    public Suit SuitType;
    public int CardValue;
    private CardSlot _parentCardSlot;

    public CardSlot ParentCardSlot { get => _parentCardSlot; set => _parentCardSlot = value; }

    private void OnDisable()
    {
        ParentCardSlot = null;
    }
}
