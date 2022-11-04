using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    [SerializeField] private InventorySO inventoryData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UIPickItem pickItem = collision.GetComponent<UIPickItem>();
        if (pickItem != null)
        {
            int reminder = inventoryData.AddItem(pickItem.data);
            if (reminder == 0)
            {
                pickItem.DestroyItem();
            }
            else
            {
                pickItem.UpdateQuantity(reminder);
            }
        }
    }
}
