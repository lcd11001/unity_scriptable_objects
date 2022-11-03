using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private UIInventoryPage inventoryUI;
    [SerializeField] private InventorySO inventoryData;

    private void Start()
    {
        inventoryUI.InitInventoryUI(inventoryData.Size);
        // inventoryData.Init();
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
}
