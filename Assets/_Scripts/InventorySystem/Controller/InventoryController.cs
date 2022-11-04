using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private UIInventoryPage inventoryUI;
        [SerializeField] private InventorySO inventoryData;
        [SerializeField] private List<InventoryItem> initItems = new List<InventoryItem>();

        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }

        private void PrepareInventoryData()
        {
            inventoryData.Init();
            foreach (var item in initItems)
            {
                if (!item.IsEmpty)
                {
                    inventoryData.AddItem(item);
                }
            }
        }

        private void PrepareUI()
        {
            inventoryUI.InitInventoryUI(inventoryData.Size);
            inventoryUI.Hide();
            RegisterEvents();
        }

        private void OnDestroy()
        {
            UnregisterEvents();
        }

        private void RegisterEvents()
        {
            Debug.Log("InventoryController::RegisterEvents");
            inventoryUI.onDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.onItemActionRequested += HandleItemActionRequest;
            inventoryUI.onStartDragging += HandleStartDragging;
            inventoryUI.onSwapItems += HandleSwapItems;

            inventoryData.onInventoryDataChanged += HandleInventoryDataChanged;
        }

        private void UnregisterEvents()
        {
            Debug.Log("InventoryController::UnregisterEvents");
            inventoryUI.onDescriptionRequested -= HandleDescriptionRequest;
            inventoryUI.onItemActionRequested -= HandleItemActionRequest;
            inventoryUI.onStartDragging -= HandleStartDragging;
            inventoryUI.onSwapItems -= HandleSwapItems;

            inventoryData.onInventoryDataChanged -= HandleInventoryDataChanged;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventoryUI.isActiveAndEnabled)
                {
                    inventoryUI.Hide();
                }
                else
                {
                    // MUST call .Show before .UpdateData
                    inventoryUI.Show();
                    HandleInventoryDataChanged(inventoryData.GetCurrentInventoryState());

                }
            }
        }

        private void HandleInventoryDataChanged(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.Image, item.Value.quantity);
            }
        }

        private void HandleSwapItems(int index1, int index2)
        {
            inventoryData.SwapItem(index1, index2);
            inventoryUI.SelectItem(index2);
        }

        private void HandleStartDragging(int index)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(index);
            if (inventoryItem.IsEmpty)
            {
                return;
            }

            inventoryUI.CreateDragItem(inventoryItem.item.Image, inventoryItem.quantity);
        }

        private void HandleItemActionRequest(int index)
        {

        }

        private void HandleDescriptionRequest(int index)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(index);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            ItemSO item = inventoryItem.item;
            inventoryUI.UpdateDescription(index, item.Image, item.Name, item.Description);
        }
    }
}