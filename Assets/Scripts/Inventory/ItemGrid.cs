using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    InventoryIten[,] inventoryItemGrid;

    public const float TileSizeWidth = 32f;
    public const float TileSizeHeight = 32f;

    [SerializeField]
    int gridSizeWidth;
    [SerializeField]
    int gridSizeHeight;

    RectTransform rectTransform;

    Vector2 mousePostionOnTheGrid;
    Vector2Int tileGridPosition = new Vector2Int();

    [SerializeField]
    GameObject inventoryItemPrefab;


    //인벤토리의 가로 세로 길이 값을 받아 생성하는 부분
    public void Init()
    {
        rectTransform = GetComponent<RectTransform>();
        inventoryItemGrid = new InventoryIten[gridSizeWidth, gridSizeHeight];
        Vector2 size = new Vector2();
        size.x = TileSizeWidth * gridSizeWidth;
        size.y = TileSizeHeight * gridSizeHeight;
        rectTransform.sizeDelta = size;
    }

    public void PlaceItem(InventoryIten itemToPlace, int x, int y)
    {
        RectTransform itemRectTransform = itemToPlace.GetComponent<RectTransform>();
        itemRectTransform.SetParent(transform);

        for(int ix = 0; ix < itemToPlace.itemData.sizeWidth; ix++)
        {
            for(int iy = 0; iy < itemToPlace.itemData.sizeHeight; iy++)
            {
                inventoryItemGrid[x + ix, y + iy] = itemToPlace;
            }
        }

        itemToPlace.positionOnGridX = x;
        itemToPlace.positionOnGridY = y;

        itemRectTransform.localPosition = CalculatePositionOfObjectOnGrid(itemToPlace, x, y);
    }

    public Vector2 CalculatePositionOfObjectOnGrid(InventoryIten item, int x, int y)
    {
        Vector2 positionOnGrid = new Vector2();
        positionOnGrid.x = TileSizeWidth * x + TileSizeWidth * item.itemData.sizeWidth / 2;
        positionOnGrid.y = -(TileSizeHeight * y + TileSizeHeight * item.itemData.sizeHeight / 2);
        return positionOnGrid;
    }

    //private void OnGUI()
    //{
    //    GUI.Label(new Rect(10, 10, 200, 20), mousePostionOnTheGrid.ToSafeString());
    //    GUI.Label(new Rect(10, 30, 200, 20), GetGridPosition(Input.mousePosition).ToString());
    //}

    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        mousePostionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
        mousePostionOnTheGrid.y = rectTransform.position.y - mousePosition.y;

        tileGridPosition.x = (int)(mousePostionOnTheGrid.x / TileSizeWidth);
        tileGridPosition.y = (int)(mousePostionOnTheGrid.y / TileSizeHeight);

        return tileGridPosition;
    }

    public InventoryIten PickUpItem(Vector2Int tillePositionOnGrid)
    {
        InventoryIten pickedItem = inventoryItemGrid[tillePositionOnGrid.x, tillePositionOnGrid.y];

        if (pickedItem == null)
        {
            return null;
        }

        ClearGridFromItem(pickedItem);

        return pickedItem;
    }

    public void ClearGridFromItem(InventoryIten pickedItem)
    {
        for (int ix = 0; ix < pickedItem.itemData.sizeWidth; ix++)
        {
            for (int iy = 0; iy < pickedItem.itemData.sizeHeight; iy++)
            {
                inventoryItemGrid[pickedItem.positionOnGridX + ix, pickedItem.positionOnGridY + iy] = null;
            }
        }
    }

    public bool PositionCheck(int x, int y)
    {
        if(x < 0 || y < 0)
        {
            return false;
        }

        if(x >= gridSizeWidth || y >= gridSizeHeight)
        {
            return false;
        }

        return true;
    }

    public bool BoundryCheck(int posX, int posY, int width, int height) 
    {

        if(PositionCheck(posX, posY) == false)
        {
            return false;
        }

        posX += width - 1;
        posY += height - 1;

        if(PositionCheck(posX, posY) == false)
        {
            return false;
        }

        return true;
    }

    public bool CheckOverlap(int posX, int posY, int sizeWidth, int sizeHeight, ref InventoryIten overlapItem)
    {
        for(int x = 0; x < sizeWidth; x++)
        {
            for(int y = 0; y < sizeHeight; y++)
            {
                if (inventoryItemGrid[posX + x, posY + y] != null)
                {
                    if(overlapItem == null)
                    {
                        overlapItem = inventoryItemGrid[posX + x, posY + y];
                    }
                    else
                    {
                        if(overlapItem != inventoryItemGrid[posX + x, posY + y])
                        {
                            return false;
                        }
                    }
                }
            }
        }

        return true;
    }

    public InventoryIten GetItem(int x, int y)
    {
        return inventoryItemGrid[x, y];
    }

    public Vector2Int? FindSpaceForObject(ItemData itemData)
    {
        int height = gridSizeHeight - itemData.sizeHeight + 1;
        int width = gridSizeWidth - itemData.sizeWidth + 1;

        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                if(CheckAvailableSpace(x, y, itemData.sizeWidth, itemData.sizeHeight) == true)
                {
                    return new Vector2Int(x, y);
                }
            }
        }

        return null;
    }

    private bool CheckAvailableSpace(int posX, int posY, int sizeWidth, int sizeHeight)
    {
        for (int x = 0; x < sizeWidth; x++)
        {
            for (int y = 0; y < sizeHeight; y++)
            {
                if (inventoryItemGrid[posX + x, posY + y] != null)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
