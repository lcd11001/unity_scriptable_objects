using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class InventorySO : ScriptableObject
{
    [SerializeField] private List<InventoryItem> inventoryItems;
    [field: SerializeField] public int Size { get; private set; } = 10;

    public void Init()
    {
        inventoryItems = new List<InventoryItem>();
        for (int i = 0; i < Size; i++)
        {
            inventoryItems.Add(InventoryItem.GetEmptyItem());
        }
    }

    public void AddItem(ItemSO item, int quantity)
    {
        int emptyIndex = inventoryItems.FindIndex(x => x.IsEmpty);
        if (emptyIndex != -1)
        {
            inventoryItems[emptyIndex] = InventoryItem.CreateItem(item, quantity);
        }
    }

    public Dictionary<int, InventoryItem> GetCurrentInventoryState()
    {
        // return inventoryItems
        // .Where(item => !item.IsEmpty)
        // .ToDictionary(keySelector: x => x.item.ID);

        return inventoryItems
        .Select((item, index) => new KeyValuePair<int, InventoryItem>(index, item))
        .Where(obj => !obj.Value.IsEmpty)
        .ToDictionary(keySelector: obj => obj.Key, elementSelector: obj => obj.Value);
    }
}

// Why struct?
// https://www.youtube.com/watch?v=Ict7bCTyRok&list=PLcRSafycjWFegXSGBBf4fqIKWkHDw_G8D&index=13&t=251s
[Serializable]
public struct InventoryItem
{
    public int quantity;
    public ItemSO item;
    public bool IsEmpty => item == null;

    public InventoryItem ChangeQuantity(int newQuantity)
    {
        return new InventoryItem
        {
            item = this.item,
            quantity = newQuantity
        };
    }

    public static InventoryItem GetEmptyItem()
    {
        return new InventoryItem
        {
            item = null,
            quantity = 0
        };
    }

    public static InventoryItem CreateItem(ItemSO item, int quantity)
    {
        return new InventoryItem
        {
            item = item,
            quantity = quantity
        };
    }
}
