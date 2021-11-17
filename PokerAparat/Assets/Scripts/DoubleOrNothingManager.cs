using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleOrNothingManager : MonoBehaviour
{
    private bool userSelectedLowCard;
    private static DoubleOrNothingManager _instance;
    private List<GameObject> drawnCards = new List<GameObject>();
    public int StartingCardOffset = 20;
    public int SpaceBetweenCards = 50;

    public static DoubleOrNothingManager Instance
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

    public void PrepareDoubleOrNothingData()
    {
        
    }

    public void DoubleOrNothingFinishAction()
    {
        ClearDrawnCards();
    }

    public void CheckIsItCorrect()
    {

    }

    public void DrawNewCard(bool isLowCard)
    {
        userSelectedLowCard = isLowCard;
        CreateNewCardItem();
    }

    void CreateNewCardItem()
    {
        GameObject cardPrefab = PoolManager.Instance.PoolOut(PoolManager.Instance.CardItem);
        drawnCards.Add(cardPrefab);
        cardPrefab.transform.SetParent(UIManager.Instance.DoubleOrNothingPanel, false);
        RectTransform rt = cardPrefab.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(StartingCardOffset + (drawnCards.Count * SpaceBetweenCards), rt.anchoredPosition.y);
        
    }

    private void ClearDrawnCards()
    {
        foreach (GameObject rowGO in drawnCards)
        {
            rowGO.transform.SetParent(null, false);
            PoolManager.Instance.PoolIn(rowGO);
        }
        drawnCards.Clear();
    }
}
