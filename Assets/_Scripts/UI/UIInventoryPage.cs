using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField] private UIInventoryItem itemPrefab;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private UIInventoryDescription itemDescription;
    [SerializeField] private UIInventoryMouseFollower mouseFollower;

    private List<UIInventoryItem> listUIItems = new List<UIInventoryItem>();
    private int currentDraggedItemIndex = -1;

    public Sprite sprite, sprite2;
    public int quantity;
    public string title, description;

    private void Awake()
    {
        // contentPanel.transform.DetachChildren();

        Hide();
        TurnOffMouseFollower();
        itemDescription.ResetDescription();
    }

    public void InitInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity, contentPanel);

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
        TurnOffMouseFollower();
    }

    private void HandleItemSwap(UIInventoryItem item)
    {
        int index = listUIItems.IndexOf(item);
        if (index == -1)
        {
            TurnOffMouseFollower();
            return;
        }

        if (currentDraggedItemIndex > -1)
        {
            listUIItems[currentDraggedItemIndex].SetData(index == 0 ? sprite : sprite2, quantity);
            listUIItems[index].SetData(index == 0 ? sprite : sprite2, quantity);
            TurnOffMouseFollower();
        }
    }

    private void TurnOffMouseFollower()
    {
        mouseFollower.Toggle(false);
        currentDraggedItemIndex = -1;
    }

    private void TurnOnMouseFollower(int index)
    {
        currentDraggedItemIndex = index;
        mouseFollower.Toggle(true);
    }

    private void HandleItemBeginDrag(UIInventoryItem item)
    {
        int index = listUIItems.IndexOf(item);
        if (index == -1)
        {
            return;
        }

        TurnOnMouseFollower(index);

        mouseFollower.SetData(index == 0 ? sprite : sprite2, quantity);
    }

    private void HandleItemSelection(UIInventoryItem item)
    {
        itemDescription.SetDescription(sprite, title, description);
        listUIItems[0].Select();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();

        listUIItems[0].SetData(sprite, quantity);
        listUIItems[1].SetData(sprite2, quantity);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        TurnOffMouseFollower();
    }
}
