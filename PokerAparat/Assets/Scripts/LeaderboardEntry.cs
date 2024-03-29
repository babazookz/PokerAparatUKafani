using System;
using System.Collections.Generic;

public class LeaderboardEntry
{
    public string uid;
    public int score = 0;

    public LeaderboardEntry()
    {
    }

    public LeaderboardEntry(string uid, int score)
    {
        this.uid = uid;
        this.score = score;
    }

    public Dictionary<string, Object> ToDictionary()
    {
        Dictionary<string, Object> result = new Dictionary<string, Object>();
        result["uid"] = uid;
        result["score"] = score;

        return result;
    }
}