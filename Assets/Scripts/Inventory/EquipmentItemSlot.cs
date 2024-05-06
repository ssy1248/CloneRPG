using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItemSlot : MonoBehaviour
{
    [SerializeField]
    EquipmentSlot equipmentSlot;

    InventoryIten itemInSlot;

    RectTransform slotRectTransfrom;

    Inventory inventory;

    private void Awake()
    {
        slotRectTransfrom = GetComponent<RectTransform>();
    }

    public bool Check(InventoryIten itemToPlace)
    {
        return equipmentSlot == itemToPlace.itemData.equipmentSlot;
    }

    public void Init(Inventory inventory)
    { 
        this.inventory = inventory;
    }

    public InventoryIten ReplaceItem(InventoryIten itemToPlace)
    {
        InventoryIten replaceItem = PickUpItem();

        PlaceItem(itemToPlace);

        return replaceItem;
    }

    public void PlaceItem(InventoryIten itemToPlace)
    {
        itemInSlot = itemToPlace;
        inventory.AddStats(itemInSlot.itemData.stats);
        RectTransform rt = itemToPlace.GetComponent<RectTransform>();
        rt.SetParent(slotRectTransfrom);
        rt.position = slotRectTransfrom.position;
    }

    internal InventoryIten PickUpItem()
    {
        InventoryIten pickupItem = itemInSlot;

        if (pickupItem != null)
        {
            inventory.SubtractStats(pickupItem.itemData.stats);
            ClearSlot(pickupItem);
        }

        return pickupItem;
    }

    private void ClearSlot(InventoryIten pickupItem)
    {
        itemInSlot = null;

        RectTransform rt = pickupItem.GetComponent<RectTransform>();
        rt.SetParent(null);
    }
}
