using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleOrNothingManager : MonoBehaviour
{
    private bool userSelectedLowCard;
    public void PrepareDoubleOrNothingData()
    {
        
    }

    public void CheckIsItCorrect()
    {

    }

    public void DealCard(bool lowCard)
    {
        userSelectedLowCard = lowCard;
    }
}
