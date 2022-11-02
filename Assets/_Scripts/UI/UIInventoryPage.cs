using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField] private UIInventoryItem itemPrefab;
    [SerializeField] private RectTransform contentPanel;

    List<UIInventoryItem> listUIItems = new List<UIInventoryItem>();

    public void InitInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);

            uiItem.onItemClicked += HandleItemSelection;
            uiItem.onItemBeginDrag += HandleItemBeginDrag;
            uiItem.onItemDroppedOn += HandleItemSwap;
            uiItem.onItemEndDrag += HandleItemEndDrag;
            uiItem.onRightMouseClicked += HandleItemShowActions;

            listUIItems.Add(uiItem);
        }
    }

    private void HandleItemShowActions(UIInventoryItem item)
    {
        
    }

    private void HandleItemEndDrag(UIInventoryItem item)
    {
        
    }

    private void HandleItemSwap(UIInventoryItem item)
    {
        
    }

    private void HandleItemBeginDrag(UIInventoryItem item)
    {
        
    }

    private void HandleItemSelection(UIInventoryItem item)
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
