using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        [SerializeField] private List<InventoryItem> inventoryItems;
        [field: SerializeField] public int Size { get; private set; } = 10;

        public event Action<Dictionary<int, InventoryItem>> onInventoryDataChanged;

        public bool IsInventoryFull => inventoryItems.Where(x => x.IsEmpty).Count() == 0;

        public void Init()
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

        private int AddItem(ItemSO item, int quantity)
        {
            if (item.IsStackable)
            {
                quantity -= AddItemToExistingSlot(item, quantity);
            }
            
            // else
            {
                while(quantity > 0 && !IsInventoryFull)
                {
                    quantity -= AddItemToFreeSlot(item, quantity);
                }
            }

            return quantity;
        }

        private int AddItemToExistingSlot(ItemSO item, int quantity)
        {
            int existIndex = inventoryItems.FindIndex(x => !x.IsEmpty && x.item.ID == item.ID && x.quantity < x.item.MaxStackSize);
            if (existIndex != -1)
            {
                int prevQuantity = inventoryItems[existIndex].quantity;
                inventoryItems[existIndex] = inventoryItems[existIndex].ChangeQuantity(quantity + prevQuantity);
                return inventoryItems[existIndex].quantity - prevQuantity;
            }
            return 0;
        }

        private int AddItemToFreeSlot(ItemSO item, int quantity)
        {
            int emptyIndex = inventoryItems.FindIndex(x => x.IsEmpty);
            if (emptyIndex != -1)
            {
                inventoryItems[emptyIndex] = InventoryItem.CreateItem(item, quantity);
                return inventoryItems[emptyIndex].quantity;
            }
            return 0;
        }

        public int AddItem(InventoryItem inventoryItem)
        {
            int remainQuantity = AddItem(inventoryItem.item, inventoryItem.quantity);
            NotifyChanged();

            return remainQuantity;
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

        public InventoryItem GetItemAt(int index)
        {
            return inventoryItems[index];
        }

        public void SwapItem(int index1, int index2)
        {
            inventoryItems.Swap(index1, index2);
            NotifyChanged();
        }

        private void NotifyChanged()
        {
            onInventoryDataChanged?.Invoke(GetCurrentInventoryState());
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
                quantity = Math.Min(newQuantity, this.item.MaxStackSize)
            };
        }

        /**
        // struct can not be modify value in list
        // use ChangeQuality instead
        public int UpdateQuantity(int delta)
        {
            int prevQuantity = this.quantity;
            this.quantity = Math.Max(0, Math.Min(this.quantity + delta, this.item.MaxStackSize));

            if (this.quantity == 0)
            {
                this.item = null;
            }

            return this.quantity;
        }
        */

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
                quantity = Math.Min(quantity, item.MaxStackSize)
            };
        }
    }
}