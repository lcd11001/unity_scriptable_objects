using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryPage : MonoBehaviour
    {
        [SerializeField] private UIInventoryItem itemPrefab;
        [SerializeField] private RectTransform contentPanel;
        [SerializeField] private UIInventoryDescription itemDescription;
        [SerializeField] private UIInventoryMouseFollower mouseFollower;

        private List<UIInventoryItem> listUIItems = new List<UIInventoryItem>();
        private int currentDraggedItemIndex = -1;

        public event Action<int> onDescriptionRequested, onItemActionRequested, onStartDragging;
        public event Action<int, int> onSwapItems;

        private void Awake()
        {
            // contentPanel.transform.DetachChildren();

            Hide();
            itemDescription.ResetDescription();
        }

        private void OnDestroy()
        {
            UnregisterEvents();
        }

        private void UnregisterEvents()
        {
            Debug.Log("UIInventoryPage::UnregisterEvents");
            foreach (var uiItem in listUIItems)
            {
                uiItem.onItemClicked -= HandleItemSelection;
                uiItem.onItemBeginDrag -= HandleItemBeginDrag;
                uiItem.onItemDroppedOn -= HandleItemSwap;
                uiItem.onItemEndDrag -= HandleItemEndDrag;
                uiItem.onRightMouseClicked -= HandleItemShowActions;
            }
        }

        private void RegisterEvents()
        {
            Debug.Log("UIInventoryPage::RegisterEvents");
            foreach (var uiItem in listUIItems)
            {
                uiItem.onItemClicked += HandleItemSelection;
                uiItem.onItemBeginDrag += HandleItemBeginDrag;
                uiItem.onItemDroppedOn += HandleItemSwap;
                uiItem.onItemEndDrag += HandleItemEndDrag;
                uiItem.onRightMouseClicked += HandleItemShowActions;
            }
        }

        public void InitInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity, contentPanel);
                uiItem.name += $" {i}";
                listUIItems.Add(uiItem);
            }

            RegisterEvents();
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (itemIndex < listUIItems.Count)
            {
                listUIItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        public void UpdateDescription(int index, Sprite image, string name, string description)
        {
            DeselectAllItems();
            SelectItem(index);

            itemDescription.SetDescription(image, name, description);
        }

        public void ResetAllItems()
        {
            foreach (var item in listUIItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }

        public void CreateDragItem(Sprite itemImage, int itemQuantity)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(itemImage, itemQuantity);
        }

        private void ResetDraggedItem()
        {
            mouseFollower.Toggle(false);
            currentDraggedItemIndex = -1;
        }

        public void ResetSelection()
        {
            itemDescription.ResetDescription();
            DeselectAllItems();
        }

        private void DeselectAllItems()
        {
            foreach (UIInventoryItem item in listUIItems)
            {
                item.Deselect();
            }
        }

        public void SelectItem(int index)
        {
            listUIItems[index].Select();
        }

        private void HandleItemShowActions(UIInventoryItem item)
        {

        }

        private void HandleItemEndDrag(UIInventoryItem item)
        {
            ResetDraggedItem();
        }

        private void HandleItemSwap(UIInventoryItem item)
        {
            int index = listUIItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }

            if (currentDraggedItemIndex != -1)
            {
                onSwapItems?.Invoke(currentDraggedItemIndex, index);
            }
        }

        private void HandleItemBeginDrag(UIInventoryItem item)
        {
            int index = listUIItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }
            HandleItemSelection(item);

            currentDraggedItemIndex = index;
            onStartDragging?.Invoke(index);
        }

        private void HandleItemSelection(UIInventoryItem item)
        {
            int index = listUIItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }

            onDescriptionRequested?.Invoke(index);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            ResetSelection();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            ResetDraggedItem();
        }
    }
}