using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHighlight : MonoBehaviour
{
    [SerializeField]
    RectTransform highlighter;

    public void SetSize(InventoryIten inventoryITem)
    {
        Vector2 size = new Vector2();
        size.x = inventoryITem.itemData.sizeWidth * ItemGrid.TileSizeWidth;
        size.y = inventoryITem.itemData.sizeHeight * ItemGrid.TileSizeHeight;
        highlighter.sizeDelta = size;
    }

    public void SetPosition(ItemGrid targetGrid, InventoryIten targetItem)
    {
        Vector2 postition = targetGrid.CalculatePositionOfObjectOnGrid(targetItem, targetItem.positionOnGridX, targetItem.positionOnGridY);

        highlighter.localPosition = postition;
    }

    public void SetParent(ItemGrid targetGrid)
    {
        if(targetGrid == null)
        {
            return;
        }
        highlighter.SetParent(targetGrid.transform);
    }

    public void SetPosition(ItemGrid targetGrid, InventoryIten targetItem, int posX, int posY)
    {
        Vector2 pos = targetGrid.CalculatePositionOfObjectOnGrid(targetItem, posX, posY);

        highlighter.localPosition = pos; 
    }

    public void Show(bool set)
    {
        highlighter.gameObject.SetActive(set);
    }
}
