using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    public ItemGrid selectedItemGrid;
    private EquipmentItemSlot selecteditemSlot;

    [SerializeField]
    MouseInput mousInput;
    Vector2 mousePosition;

    Vector2Int positionOnGrid;
    InventoryIten selectedItem;
    InventoryIten overlapItem;
    RectTransform selectedItemRectTransform;

    [SerializeField]
    List<ItemData> itemDatas;
    [SerializeField]
    GameObject inventoryItemPrefab;
    [SerializeField]
    Transform targetCanvas;

    [SerializeField]
    InventoryHighlight inventoryHighlight;
    [SerializeField]
    RectTransform selectedItemParent;

    InventoryIten itemToHighlight;
    private bool isOverUIElement;

    public EquipmentItemSlot SelectedTtemSlot
    {
        get => selecteditemSlot;
        set
        {
            selecteditemSlot = value;
        }
    }

    public ItemGrid SelectedItemGrid
    {
        get => selectedItemGrid;
        set
        {
            selectedItemGrid = value;
            inventoryHighlight.SetParent(value);
        }
    }

    private void Update()
    {
        isOverUIElement = EventSystem.current.IsPointerOverGameObject();

        ProcessMousePosition();

        ProcessMouseInput();

        HandleHighlight();

        //if(selectedItem == null)
        //{
        //    return;
        //}

        //if(Input.GetKeyDown(KeyCode.A))
        //{
        //    CreateRandomItem();
        //}

        //if(Input.GetKeyDown(KeyCode.Z))
        //{
        //    InsertRandomItem();
        //}
    }

    private void ProcessMousePosition()
    {
        mousePosition = mousInput.mouseInputPosition;
    }

    private void InsertRandomItem()
    {
        if (selectedItemGrid == null)
        {
            return;
        }

        CreateRandomItem();
        InventoryIten itemToInsert = selectedItem;
        selectedItem = null;
        InsertItem(itemToInsert);
    }

    private void InsertItem(InventoryIten itemToInsert)
    {
        Vector2Int? posOnGrid = SelectedItemGrid.FindSpaceForObject(itemToInsert.itemData);

        if (posOnGrid == null)
        {
            return;
        }

        selectedItemGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
    }

    Vector2Int oldPosition;

    private void HandleHighlight()
    {
        if(selecteditemSlot != null)
        {
            inventoryHighlight.Show(false);
            return;
        }

        if (selectedItemGrid == null)
        {
            inventoryHighlight.Show(false);
            return;
        }

        Vector2Int postitionOnGrid = GetTileGridPosition();

        if(postitionOnGrid == oldPosition)
        {
            return;
        }

        if(selectedItemGrid.PositionCheck(postitionOnGrid.x, postitionOnGrid.y) == false)
        {
            return;
        }

        oldPosition = postitionOnGrid;

        if (selectedItem == null)
        {
            itemToHighlight = selectedItemGrid.GetItem(postitionOnGrid.x, postitionOnGrid.y);

            if(itemToHighlight != null)
            {
                inventoryHighlight.Show(true);
                inventoryHighlight.SetSize(itemToHighlight);
                inventoryHighlight.SetPosition(selectedItemGrid, itemToHighlight);
            }
            else
            {
                inventoryHighlight.Show(false);
            }
        }
        else
        {
            inventoryHighlight.Show(selectedItemGrid.BoundryCheck(positionOnGrid.x, positionOnGrid.y, selectedItem.itemData.sizeWidth, selectedItem.itemData.sizeHeight));
            inventoryHighlight.SetSize(selectedItem);
            inventoryHighlight.SetPosition(selectedItemGrid, selectedItem, positionOnGrid.x, positionOnGrid.y);
        }
    }

    private void CreateRandomItem()
    {
        if(selectedItem != null)
        {
            return;
        }

        int selectedItemID = UnityEngine.Random.Range(0, itemDatas.Count);
        InventoryIten newItem = CreateNewInventoryItem(itemDatas[selectedItemID]);
        SelectItem(newItem);
    }

    public InventoryIten CreateNewInventoryItem(ItemData itemData)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab);

        InventoryIten newInventoryItem = newItemGo.GetComponent<InventoryIten>();

        RectTransform newItemRectTransform = newItemGo.GetComponent<RectTransform>();
        newItemRectTransform.SetParent(targetCanvas);

        newInventoryItem.Set(itemData);

        return newInventoryItem;
    }

    public void SelectItem(InventoryIten inventoryItem)
    {
        selectedItem = inventoryItem;
        selectedItemRectTransform = inventoryItem.GetComponent<RectTransform>();
        selectedItemRectTransform.SetParent(selectedItemParent);
    }

    public void ProcessLMBPress(InputAction.CallbackContext context)
    {
        if(context.phase != InputActionPhase.Started)
        {
            return;
        }

        if (selectedItemGrid == null && selecteditemSlot == null)
        {
            if (isOverUIElement)
            {
                return;
            }
            ThrowItemAwayProcess();
        }

        if (selectedItemGrid != null)
        {
            ItemGridInput();
        }

        if (selecteditemSlot != null)
        {
            ItemSlotInput();
        }
    }

    private void ProcessMouseInput()
    {
        if (selectedItem != null)
        {
            selectedItemRectTransform.position = mousePosition;
            //selectedItemRectTransform = inventoryItemPrefab.GetComponent<RectTransform>();
        }
       
    }

    private void ThrowItemAwayProcess()
    {
        if(selectedItem == null)
        {
            return;
        }

        ItemSpawnManager.instance.SpawnItem(GameManager.Instance.playerObjcet.transform.position , selectedItem.itemData);
        DestroySelectedItemObject();
        NullSelectedItem();
    }

    private void DestroySelectedItemObject()
    {
        Destroy(selectedItemRectTransform.gameObject);
    }

    private void ItemSlotInput()
    {
        if(selectedItem != null)
        {
            PlaceItemIntoSlot();
        }
        else
        {
            PickUpItemFromSlot();
        }
    }

    private void PickUpItemFromSlot()
    {
        InventoryIten item = selecteditemSlot.PickUpItem();

        if(item != null)
        {
            SelectItem(item);
        }
    }

    private void PlaceItemIntoSlot()
    {
        if(selecteditemSlot.Check(selectedItem) == false)
        {
            return;
        }

        InventoryIten replacedItem = selecteditemSlot.ReplaceItem(selectedItem);

        if(replacedItem == null)
        {
            NullSelectedItem();
        }
        else
        {
            SelectItem(replacedItem);
        }
        
    }

    private void NullSelectedItem()
    {
        selectedItem = null;
        selectedItemRectTransform = null;
    }

    private void ItemGridInput()
    {
        positionOnGrid = GetTileGridPosition();
        if (selectedItem == null)
        {
            InventoryIten itemToSelect = selectedItem = selectedItemGrid.PickUpItem(positionOnGrid);
            if (selectedItem != null)
            {
                SelectItem(itemToSelect);
            }
        }
        else
        {
            PlaceItemInput();
        }
    }

    Vector2Int GetTileGridPosition()
    {
        Vector2 position = mousePosition;
        if(selectedItem != null)
        {
            position.x -= (selectedItem.itemData.sizeHeight - 1) * ItemGrid.TileSizeWidth / 2;
            position.y += (selectedItem.itemData.sizeWidth - 1) * ItemGrid.TileSizeHeight / 2;
        }

        return selectedItemGrid.GetTileGridPosition(position);
    }

    private void PlaceItemInput()
    {
        if (selectedItemGrid.BoundryCheck(positionOnGrid.x, 
            positionOnGrid.y, 
            selectedItem.itemData.sizeWidth, 
            selectedItem.itemData.sizeHeight) == false)
        {
            return;
        }

        if(selectedItemGrid.CheckOverlap(positionOnGrid.x, positionOnGrid.y, 
            selectedItem.itemData.sizeWidth, selectedItem.itemData.sizeHeight,
            ref overlapItem) == false)
        {
            overlapItem = null;
            return;
        }

        if(overlapItem != null)
        {
            selectedItemGrid.ClearGridFromItem(overlapItem);
        }

        selectedItemGrid.PlaceItem(selectedItem, positionOnGrid.x, positionOnGrid.y);
        NullSelectedItem();

        if(overlapItem != null)
        {
            selectedItem = overlapItem;
            selectedItemRectTransform = selectedItem.GetComponent<RectTransform>();
            overlapItem = null;
        }
    }
}
