using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAccount : MonoBehaviour
{
    public int PlayerBalance = 0;
    public int Highscore = 0;
    public Text PlayerBalanceText;
    private static PlayerAccount _instance;

    public static PlayerAccount Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        PlayerBalance = PrefsManager.PlayerBalance;
        PlayerBalanceText.text = PlayerBalance.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddCredits(int credits)
    {
        PlayerBalance += credits;
        PrefsManager.PlayerBalance = PlayerBalance;
        PlayerBalanceText.text = PlayerBalance.ToString();

        if (PrefsManager.PlayerBalance > PrefsManager.PersonalHighscore)
        {
            FirebaseCustomInitialization.Instance.WriteNewScore(PrefsManager.Username, PrefsManager.PlayerBalance);
            PrefsManager.PersonalHighscore = PrefsManager.PlayerBalance;
        }
    }
}
