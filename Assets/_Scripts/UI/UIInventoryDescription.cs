using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryDescription : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;

    public string ItemTitle { get => title.text; set => title.text = value; }
    public string ItemDescription { get => description.text; set => description.text = value; }
    public Sprite ItemImage { get => itemImage.sprite; set => itemImage.sprite = value; }

    private void Awake()
    {
        ResetDescription();
    }

    public void ResetDescription()
    {
        this.itemImage.gameObject.SetActive(false);
        this.ItemTitle = "";
        this.ItemDescription = "";
    }

    public void SetDescription(Sprite sprite, string itemName, string itemDescription)
    {
        this.itemImage.gameObject.SetActive(true);

        this.ItemImage = sprite;
        this.ItemTitle = itemName;
        this.ItemDescription = itemDescription;
    }
}
