using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    [SerializeField] private EquippableItemSO weapon;
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private List<ItemParameter> parametersToModify;
    [SerializeField] private List<ItemParameter> itemCurrentState;

    public void SetWeapon(EquippableItemSO weaponItemSO, List<ItemParameter> itemState)
    {
        if (weapon != null)
        {
            inventoryData.AddItem(InventoryItem.CreateItem(weapon, 1, itemCurrentState));
        }

        this.weapon = weaponItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        ModifyParameters();
    }

    private void ModifyParameters()
    {
        foreach(var param in parametersToModify)
        {
            if (itemCurrentState.Contains(param))
            {
                int index = itemCurrentState.IndexOf(param);
                float newValue = itemCurrentState[index].value + param.value;
                itemCurrentState[index] = new ItemParameter
                {
                    itemParameter = param.itemParameter,
                    value = newValue
                };
            }
        }
    }
}
