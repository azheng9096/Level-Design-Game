using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject InventoryUICanvas;

    [SerializeField] Transform InventorySlotPanel;
    [SerializeField] GameObject InventorySlotPrefabs;

    [SerializeField] InventoryItemDisplay ItemDisplay;


    // Start is called before the first frame update
    void Start()
    {
        InventoryManager.instance.InventoryChangedCallback += ListItems;
    }

    void OnDestroy() {
        InventoryManager.instance.InventoryChangedCallback -= ListItems;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) {
            ToggleInventoryUI();
        }
    }

    void ListItems() {
        foreach (Transform child in InventorySlotPanel) {
            Destroy(child.gameObject);
        }

        foreach (Item item in InventoryManager.instance.inventory) {
            GameObject obj = Instantiate(InventorySlotPrefabs, InventorySlotPanel);

            InventorySlot slot = obj.GetComponent<InventorySlot>();
            slot.Set(item);

            slot.OnItemClicked += HandleModuleClick;
        }
    }

    void HandleModuleClick(InventorySlot inventorySlot) {
        ItemDisplay.DisplayItem(inventorySlot.item);
    }

    public void ToggleInventoryUI() {
        bool uiIsActive = InventoryUICanvas.activeInHierarchy;
        
        if (uiIsActive) {
            ItemDisplay.ResetDisplay();
        }
        
        InventoryUICanvas.SetActive(!uiIsActive);
    }
}
