using System.Collections;
using System.Collections.Generic;
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
}
