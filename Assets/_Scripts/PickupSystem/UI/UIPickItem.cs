using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D), typeof(AudioSource))]
[ExecuteInEditMode]
public class UIPickItem : MonoBehaviour
{
    [SerializeField] private ItemSO item;
    [SerializeField] private int quantity = 1;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float duration = 0.3f;

    public InventoryItem data
    {
        get => InventoryItem.CreateItem(item, quantity);
        set
        {
            item = value.item;
            quantity = value.quantity;
        }
    }

    private void Start()
    {
        Debug.Log("Start");
        SetSpriteImage();
    }

    private void OnValidate()
    {
        Debug.Log("OnValidate");
        SetSpriteImage();
    }

    private void SetSpriteImage()
    {
        if (item != null)
        {
            GetComponent<SpriteRenderer>().sprite = item.Image;
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
