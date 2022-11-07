using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D), typeof(AudioSource))]
[ExecuteInEditMode]
public class UIPickItem : MonoBehaviour
{
    [SerializeField] private ItemSO item;
    [SerializeField] private int quantity = 1;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private TMP_Text textQuantity;

    public InventoryItem data
    {
        get => InventoryItem.CreateItem(item, quantity);
    }

    public void UpdateQuantity(int newQuantity)
    {
        quantity = newQuantity;

        RefreshData();
    }

    private void Start()
    {
        Debug.Log("Start");
        RefreshData();
    }

    private void OnValidate()
    {
        Debug.Log("OnValidate");
        RefreshData();
    }

    private void RefreshData()
    {
        if (item != null)
        {
            GetComponent<SpriteRenderer>().sprite = item.Image;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = null;
        }

        if (textQuantity != null)
        {
            textQuantity.text = $"x{quantity}";
        }
    }

    public void DestroyItem()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AnimateItemPickup());
    }

    private IEnumerator AnimateItemPickup()
    {
        audioSource.Play();
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;

        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / duration);
            yield return null;
        }
        transform.localScale = endScale;
        Destroy(gameObject);
    }

}
