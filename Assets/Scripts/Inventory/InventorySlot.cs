using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Item item;

    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI qty;

    public event Action<InventorySlot> OnItemClicked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Set(Item newItem) {
        item = newItem;

        icon.sprite = item.sprite;
        qty.text = ""; // placeholder for now
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.pointerId == -1) {
            OnItemClicked?.Invoke(this);
        }
    }
}
