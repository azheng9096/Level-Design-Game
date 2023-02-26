using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemDisplay : MonoBehaviour
{
    Item item;

    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI description;

    // Start is called before the first frame update
    void Start()
    {
        ResetDisplay();
    }

    public void DisplayItem(Item item) {
        this.item = item;
        Display();
    }

    void Display() {
        icon.gameObject.SetActive(true);

        if (item.detailSprite != null)
            icon.sprite = item.detailSprite;
        else
            icon.sprite = item.sprite;

        itemName.text = item.itemName;
        description.text = item.description;
    }

    public void ResetDisplay() {
        item = null;

        icon.gameObject.SetActive(false);

        itemName.text = "Item Name";
        description.text = "Item description";

    }
}
