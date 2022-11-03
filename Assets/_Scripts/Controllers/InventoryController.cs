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

        private void Start()
        {
            PrepareUI();
            // inventoryData.Init();
        }

        private void PrepareUI()
        {
            inventoryUI.InitInventoryUI(inventoryData.Size);
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
        }

        private void UnregisterEvents()
        {
            Debug.Log("InventoryController::UnregisterEvents");
            inventoryUI.onDescriptionRequested -= HandleDescriptionRequest;
            inventoryUI.onItemActionRequested -= HandleItemActionRequest;
            inventoryUI.onStartDragging -= HandleStartDragging;
            inventoryUI.onSwapItems -= HandleSwapItems;
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
                    foreach (var itemData in inventoryData.GetCurrentInventoryState())
                    {
                        Debug.Log($"item key {itemData.Key} value sprite {itemData.Value.item.Image.name} quantity {itemData.Value.quantity}");
                        inventoryUI.UpdateData(itemData.Key, itemData.Value.item.Image, itemData.Value.quantity);
                    }

                }
            }
        }

        private void HandleSwapItems(int currentIndex, int index)
        {

        }

        private void HandleStartDragging(int index)
        {

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