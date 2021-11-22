using System;
using UnityEngine;

public class PrefsManager : MonoBehaviour
{
    public static bool Sound
    {
        get
        {
            return PlayerPrefs.GetInt("Sound", 1) == 1 ? true : false;
        }
        set
        {
            PlayerPrefs.SetInt("Sound", value ? 1 : 0);
        }
    }

    public static int PersonalHighscore
    {
        get
        {
            return PlayerPrefs.GetInt("PersonalHighscore", 0);
        }
        set
        {
            PlayerPrefs.SetInt("PersonalHighscore", value);
        }
    }

    public static int PlayerBalance
    {
        get
        {
            return PlayerPrefs.GetInt("PlayerBalance", 1000);
        }
        set
        {
            PlayerPrefs.SetInt("PlayerBalance", value);
        }
    }

    public static string Username
    {
        get
        {
            return PlayerPrefs.GetString("Username", "GOST");
        }
        set
        {
            PlayerPrefs.SetString("Username", value);
        }
    }

    public static string OldUsername
    {
        get
        {
            return PlayerPrefs.GetString("Username", "old_username");
        }
        set
        {
            PlayerPrefs.SetString("Username", value);
        }
    }

    public static string LastFreeCreditDatetime
    {
        get
        {
            return PlayerPrefs.GetString("LastFreeCreditDatetime", DateTime.UtcNow.AddTicks(-40000000000).Ticks.ToString());
        }
        set
        {
            PlayerPrefs.SetString("LastFreeCreditDatetime", value);
        }
    }
}
