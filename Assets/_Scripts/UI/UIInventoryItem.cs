using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour
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
        this.borderImage.enabled = false;
    }

    public void Select()
    {
        this.borderImage.enabled = true;
    }

    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);
        this.empty = true;
    }

    public void SetData(Sprite sprite, int quantity)
    {
        this.itemImage.gameObject.SetActive(true);
        this.ItemImage = sprite;
        this.ItemQuantity = $"{quantity}";
        this.empty = false;
    }

    public void OnBeginDrag()
    {
        if (empty) return;
        onItemBeginDrag?.Invoke(this);
    }

    public void OnDrop()
    {
        onItemDroppedOn?.Invoke(this);
    }

    public void OnEndDrag()
    {
        onItemEndDrag?.Invoke(this);
    }

    public void OnPointerClicked(BaseEventData data)
    {
        if (empty) return;

        PointerEventData pointerData = (PointerEventData)data;
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            onRightMouseClicked?.Invoke(this);
        }
        else
        {
            onItemClicked?.Invoke(this);
        }
    }
}