using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;



[CreateAssetMenu]
public class InventorySO : ScriptableObject
{
    [SerializeField]
    private List<InventoryItem> inventoryItems;

    [field: SerializeField]
    public int Size { get; private set; } = 10;

    //public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

    public void Initialize()
    {
        inventoryItems = new List<InventoryItem>();
        for (int i = 0; i < Size; i++)
        {
            inventoryItems.Add(InventoryItem.GetEmptyItem());
        }
    }

    //public int AddItem(Item item, int quantity, List<ItemParameter> itemState = null)
    //{
    //    if (item.IsStackable == false)
    //    {
    //        for (int i = 0; i < inventoryItems.Count; i++)
    //        {
    //            while (quantity > 0 && IsInventoryFull() == false)
    //            {
    //                quantity -= AddItemToFirstFreeSlot(item, 1, itemState);
    //            }
    //            InformAboutChange();
    //            return quantity;
    //        }
    //    }
    //    quantity = AddStackableItem(item, quantity);
    //    InformAboutChange();
    //    return quantity;
    //}

    public InventoryItem GetItemAt(int itemIndex)
    {
        return inventoryItems[itemIndex];
    }

    public Dictionary<int, InventoryItem> GetCurrentInventoryState()
    {
        Dictionary<int, InventoryItem> returnValue =
            new Dictionary<int, InventoryItem>();

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
                continue;
            returnValue[i] = inventoryItems[i];
        }
        return returnValue;
    }

}

[Serializable]
public struct InventoryItem
{
    public int quantity;
    public Item item;
    //public List<ItemParameter> itemState;
    public bool IsEmpty => item == null;

    public InventoryItem ChangeQuantity(int newQuantity)
    {
        return new InventoryItem
        {
            item = this.item,
            quantity = newQuantity,
            //itemState = new List<ItemParameter>(this.itemState)
        };
    }

    public static InventoryItem GetEmptyItem()
        => new InventoryItem
        {
            item = null,
            quantity = 0,
            //itemState = new List<ItemParameter>()
        };
}
