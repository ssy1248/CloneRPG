using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentItemSlotInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    InventoryController inventoryController;
    EquipmentItemSlot slot;

    private void Awake()
    {
        inventoryController = FindObjectOfType<InventoryController>();
        slot = GetComponent<EquipmentItemSlot>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inventoryController.SelectedTtemSlot = slot;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventoryController.SelectedTtemSlot = null;
    }
}
