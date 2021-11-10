using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IPointerClickHandler
{
    public enum SlotNumberEnum { One = 1, Two = 2, Three = 3, Four = 4, Five = 5 }
    public SlotNumberEnum SlotNumber;
    public Text HoldText;
    public Transform CardBack;
    public Card DrawnCard;
    public bool IsLocked = false;
    public Image BackgroundWhenLocked;

    private void Awake()
    {
        HoldText.gameObject.SetActive(IsLocked);
        DOTween.Init();
    }

    private void Start()
    {
        BackgroundWhenLocked.color = UIManager.Instance.SelectedCardBackgroundColor;
    }

    public void ShowCardBack()
    {
        CardBack.DORotate(new Vector3(0f, 0f, 0f), .75f, RotateMode.Fast).Play();
    }

    public void HideCardBack()
    {
        CardBack.DORotate(new Vector3(0f, 180f, 0f), .75f, RotateMode.Fast).Play();
    }

    #region IPointerClickHandler implementation

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CardDealer.Instance.DealRound == CardDealer.DealRoundEnum.Zero || CardDealer.Instance.DealRound == CardDealer.DealRoundEnum.Second)
        {
            return;
        }

        if (DrawnCard == null)
        {
            return;
        }

        IsLocked = !IsLocked;
        HoldText.gameObject.SetActive(IsLocked);
        BackgroundWhenLocked.enabled = IsLocked;
    }

    public void ResetSlot()
    {
        IsLocked = false;
        HoldText.gameObject.SetActive(IsLocked);
        BackgroundWhenLocked.enabled = IsLocked;
    }

    #endregion

    public void LockCard()
    {
        if (!IsLocked)
        {
            IsLocked = true;
            HoldText.gameObject.SetActive(IsLocked);
            BackgroundWhenLocked.enabled = IsLocked;
        }
    }
}
