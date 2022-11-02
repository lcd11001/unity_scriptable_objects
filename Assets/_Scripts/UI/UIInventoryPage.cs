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


    List<UIInventoryItem> listUIItems = new List<UIInventoryItem>();

    public Sprite sprite;
    public int quantity;
    public string title, description;

    private void Awake()
    {
        // contentPanel.transform.DetachChildren();

        Hide();
        mouseFollower.Toggle(false);
        itemDescription.ResetDescription();
    }

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
        mouseFollower.Toggle(false);
    }

    private void HandleItemSwap(UIInventoryItem item)
    {

    }

    private void HandleItemBeginDrag(UIInventoryItem item)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite, quantity);
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
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
