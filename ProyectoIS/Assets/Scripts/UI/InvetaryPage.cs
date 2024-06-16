using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class InvetaryPage : MonoBehaviour
{
    [SerializeField]
    private UIInvetoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private InventoryDescription itemDescription;

    [SerializeField]
    private MouseFollower mouseFollower;

    List<UIInvetoryItem> listOfItems= new List<UIInvetoryItem>();

    private int intcurrentlyDraggedIndex = -1;
    public event Action<int> OnDescriptionRequested,
                OnItemActionRequested,
                OnStartDragging;

    public event Action<int, int> OnSwapItems;


    public void UpdateData(int itemIndex,
            Sprite itemImage, int itemQuantity)
    {
        if (listOfItems.Count > itemIndex)
        {
            listOfItems[itemIndex].SetData(itemImage, itemQuantity);
        }
    }
    private void Awake()
    {
       
        itemDescription.ResetDescription();
        mouseFollower.Toggle(false);
    }

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
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseBtnClick += HandleShowItemActions;
        }
    }

    private void HandleShowItemActions(UIInvetoryItem InventoryItemUI)
    {
        throw new NotImplementedException();
    }

    private void HandleEndDrag(UIInvetoryItem InventoryItemUI)
    {
        mouseFollower.Toggle(false);
    }

    private void HandleSwap(UIInvetoryItem InventoryItemUI)
    {
        int index = listOfItems.IndexOf(InventoryItemUI);
        if (index == -1)
        {
            return;
        }
        OnSwapItems?.Invoke(intcurrentlyDraggedIndex, index);
        //HandleItemSelection(inventoryItemUI);

    }

    private void HandleBeginDrag(UIInvetoryItem InventoryItemUI)
    {
        int index = listOfItems.IndexOf(InventoryItemUI);
        if (index == -1)
            return;
        intcurrentlyDraggedIndex = index;
        HandleItemSelection(InventoryItemUI);
        OnStartDragging?.Invoke(index);

        //mouseFollower.SetData(image, quantity);
    }

    private void HandleItemActions(UIInvetoryItem InventoryItemUI)
    {
        
    }

    private void HandleItemSelection(UIInvetoryItem InventoryItemUI)
    {
        int index = listOfItems.IndexOf(InventoryItemUI);
        if (index == -1)
            return;
        OnDescriptionRequested?.Invoke(index);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();
        ResetSelections();
    }

    public void ResetSelections()
    {
        itemDescription.ResetDescription();
        DeselectAllItems();
    }

    private void DeselectAllItems()
    {
        foreach (UIInvetoryItem item in listOfItems)
        {
            item.Deselect();
        }
        //actionPanel.Toggle(false);
    }

    internal void ResetAllItems()
    {
        foreach (var item in listOfItems)
        {
            item.ResetData();
            item.Deselect();
        }
    }
    internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
    {
        itemDescription.SetDescription(itemImage, name, description);
        DeselectAllItems();
        listOfItems[itemIndex].Select();
    }

    public void ResetSelection()
    {
        itemDescription.ResetDescription();
        DeselectAllItems();
    }

    public void Hide() 
    {
        gameObject.SetActive(false);
        ResetDraggedItem();
    }

    public void CreateDraggedItem(Sprite sprite, int quantity)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite, quantity);
    }
    private void ResetDraggedItem()
    {
        mouseFollower.Toggle(false);
        intcurrentlyDraggedIndex = -1;
    }
}
