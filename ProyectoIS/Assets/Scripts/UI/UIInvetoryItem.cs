using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class UIInvetoryItem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image itemImage;
     [SerializeField] TMP_Text quantityNumber;
     [SerializeField] Image borderImage;

    public event Action<UIInvetoryItem> OnItemClicked,OnRightMouseBtnClick;

    private bool empty = true;


    public void Start()
    {
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);
        empty = true;

    }


    public void Deselect()
    {

        borderImage.enabled=false;    

        

    }

    public void setData(Sprite sprite, int quantity)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.quantityNumber.text = quantity + "";
        empty = false;

    }

    public void Select()
    {
        borderImage.enabled = true;
    }


    public void OnPointerClick(BaseEventData data)
    {
        

        PointerEventData pointerEventData = (PointerEventData)data;

        if (pointerEventData.button == PointerEventData.InputButton.Right)
        {

            OnRightMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }

    }

    


}
