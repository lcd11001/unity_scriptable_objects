using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text quantityTxt;

        [SerializeField] private Image borderImage;

        public event Action<UIInventoryItem> onItemClicked, onItemDroppedOn, onItemBeginDrag, onItemEndDrag, onRightMouseClicked;

        private bool empty = true;

        public Sprite ItemImage { get => itemImage.sprite; set => itemImage.sprite = value; }
        public string ItemQuantity { get => quantityTxt.text; set => quantityTxt.text = value; }

        public void Awake()
        {
            ResetData();
            Deselect();
        }

        public void Deselect()
        {
            if (!empty)
            {
                borderImage.enabled = false;
            }
        }

        public void Select()
        {
            if (!empty)
            {
                borderImage.enabled = true;
            }
        }

        public void ResetData()
        {
            itemImage.gameObject.SetActive(false);
            borderImage.enabled = false;
            empty = true;
        }

        public void SetData(Sprite sprite, int quantity)
        {
            itemImage.gameObject.SetActive(true);
            ItemImage = sprite;
            ItemQuantity = $"{quantity}";
            empty = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // we need deselect other item, when click on empty item.
            // if (empty) return;

            PointerEventData pointerData = eventData;
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                onRightMouseClicked?.Invoke(this);
            }
            else
            {
                onItemClicked?.Invoke(this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (empty) return;
            onItemBeginDrag?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            onItemEndDrag?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                onItemDroppedOn?.Invoke(this);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            // https://docs.unity3d.com/2019.1/Documentation/ScriptReference/EventSystems.IBeginDragHandler.html
            // Note: You need to implement IDragHandler in addition to IBeginDragHandler.
        }
    }
}