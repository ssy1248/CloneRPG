using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpInteractableObject : MonoBehaviour
{
    [SerializeField]
    int coinCount;
    [SerializeField]
    ItemData itemData;

    private void Start()
    {
        GetComponent<InteractableObject>().Subscribe(PickUp);
    }

    public void PickUp(Character character)
    {
        Inventory inventory = character.GetComponent<Inventory>();

        if (inventory == null)
        {
            Debug.LogWarning("To interact with this object, this character need an Inventory");
            return;
        }

        inventory.AddCurrency(coinCount);

        if(itemData != null)
        {
            inventory.AddItem(itemData);
        }

        Debug.Log("You are trying to pick up me!");
        Destroy(gameObject);
    }

    public void SetItem(ItemData itemToSpwan)
    {
        itemData = itemToSpwan;
    }
}
