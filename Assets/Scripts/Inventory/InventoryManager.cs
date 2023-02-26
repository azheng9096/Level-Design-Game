using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<Item> inventory = new List<Item>();

    public delegate void OnInventoryChanged();
    public OnInventoryChanged InventoryChangedCallback;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void AddItem(Item item) {
        inventory.Add(item);

        InventoryChangedCallback?.Invoke();
    }

    public void RemoveItem(Item item) {
        inventory.Remove(item);

        InventoryChangedCallback?.Invoke();
    }
}
