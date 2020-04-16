using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardSlot : MonoBehaviour
{
    public enum SlotNumberEnum { One = 1, Two = 2, Three = 3, Four = 4, Five = 5 }
    public SlotNumberEnum SlotNumber;
    public Text HoldText;
    public Transform CardBack;

    private void Awake()
    {
        HoldText.gameObject.SetActive(false);
        DOTween.Init();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowCardBack()
    {
        CardBack.DORotate(new Vector3(0f, 0f, 0f), 1f, RotateMode.Fast).Play();
    }

    public void HideCardBack()
    {
        CardBack.DORotate(new Vector3(0f, 180f, 0f), 1f, RotateMode.Fast).Play();
    }
}
