using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardItem : MonoBehaviour
{
    public Text PositionText, PlayerUsername, Score;

    public void PrepareItem(LeaderboardEntry le, int position)
    {
        PositionText.text = position.ToString();
        PlayerUsername.text = le.uid;
        Score.text = le.score.ToString();
    }
}
