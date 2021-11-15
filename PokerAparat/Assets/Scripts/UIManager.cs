using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Color SelectedCardBackgroundColor;
    private static UIManager _instance;
    public Transform DoubleOrNothingPanel;
    public Transform DoubleOrNothingTopPanel;
    public Transform DoubleOrNothingButtonPanel;
    public Transform MainSlotPanel;
    public Transform GoalsTopPanel;
    public Transform MainSlotButtonPanel;
    public Font AppFont;

    public static UIManager Instance
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleDoubleOrNothingScreen()
    {
        DoubleOrNothingPanel.gameObject.SetActive(!DoubleOrNothingPanel.gameObject.activeSelf);
        DoubleOrNothingTopPanel.gameObject.SetActive(!DoubleOrNothingTopPanel.gameObject.activeSelf);
        DoubleOrNothingButtonPanel.gameObject.SetActive(!DoubleOrNothingButtonPanel.gameObject.activeSelf);
    }

    public void ToggleMainSlotScreen()
    {
        MainSlotPanel.gameObject.SetActive(!MainSlotPanel.gameObject.activeSelf);
        GoalsTopPanel.gameObject.SetActive(!GoalsTopPanel.gameObject.activeSelf);
        MainSlotButtonPanel.gameObject.SetActive(!MainSlotButtonPanel.gameObject.activeSelf);
    }
}
