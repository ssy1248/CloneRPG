using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryIten : MonoBehaviour
{
    public ItemData itemData;

    public int positionOnGridX;
    public int positionOnGridY;

    internal void Set(ItemData itemData)
    {
        this.itemData = itemData;
        GetComponent<Image>().sprite = itemData.icon;

        Vector2 size = new Vector2(itemData.sizeWidth * ItemGrid.TileSizeWidth, itemData.sizeHeight * ItemGrid.TileSizeHeight);
        GetComponent<RectTransform>().sizeDelta = size;
    }
}
