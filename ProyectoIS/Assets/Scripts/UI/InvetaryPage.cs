using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvetaryPage : MonoBehaviour
{
    [SerializeField]
    private UIInvetoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    List<UIInvetoryItem> listOfItems= new List<UIInvetoryItem>();


    public void InitilizeInventoryUi(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UIInvetoryItem uiItem= Instantiate(itemPrefab,Vector3.zero,Quaternion.identity);
            uiItem.transform.SetParent(contentPanel, false);
            uiItem.transform.localScale = new Vector3(1, 1, 1);
            uiItem.transform.SetParent(contentPanel);
            listOfItems.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnRightMouseBtnClick += HandleItemActions;
        }
    }

    private void HandleItemActions(UIInvetoryItem item)
    {
        Debug.Log("HOLA");
        Debug.Log(item.name);
    }

    private void HandleItemSelection(UIInvetoryItem item)
    {
        Debug.Log(item.name);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide() 
    {
        gameObject.SetActive(false);
    }
}
