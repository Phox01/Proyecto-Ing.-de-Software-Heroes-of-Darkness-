using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    // Start is called before the first frame update
    [SerializeField] private Image itemImage;
    [SerializeField] TMP_Text quantityNumber;
    [SerializeField] Image borderImage;

    public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick;

    private bool empty = true;


    public void Start()
    {
        Deselect();
    }

    public void ResetData()
    {
        if (itemImage != null)
        {
            this.itemImage.gameObject.SetActive(false);
            empty = true;
        }

    }


    public void Deselect()
    {
        if (itemImage != null)
        {
            borderImage.enabled = false;
        }


    }

    public void SetData(Sprite sprite, int quantity)
    {
        if (this == null || sprite == null)
    {
        Debug.LogWarning("UIInventoryItem or sprite is null");
        return;
    }
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.quantityNumber.text = quantity + "";
        empty = false;

    }

    public void Select()
    {
        borderImage.enabled = true;
    }



    public void OnPointerClick(PointerEventData pointerData)
    {
        if (pointerData.button == PointerEventData.InputButton.Right)
        {

            OnRightMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (empty)
            return;
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {

    }
    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }
}
