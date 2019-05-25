using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    List<Item> animals = new List<Item>();
    public Vector2Int size;
    public Slot[,] slots;
    public GameObject slotPrefab;

    private float sWidth;
    private float sHeight;

    public Vector2 bounds;
    private GridLayoutGroup grid;

    Rect rect;


    void Start()
    {
        rect = this.GetComponent<RectTransform>().rect;
        bounds = new Vector2(rect.width, rect.height);

        sWidth = rect.width / size.x;
        sHeight = rect.height / size.y;
        grid = GetComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(sWidth, sHeight);

        slots = new Slot[size.x, size.y];
        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                Vector2 pos = new Vector2((rect.position.x - bounds.x / 2) + sWidth * x, (rect.position.y + bounds.y / 2) + sHeight * y);
                GameObject s = Instantiate(slotPrefab, pos, Quaternion.identity, this.transform);
                slots[x, y] = s.GetComponent<Slot>();
            }
        }
    }

    public GameObject test;
    public RectTransform canvas;
    public void AddItem(PointerEventData ped, Transform piece)
    {
        Vector2Int selectedSlot = GetSlot(ped);
        Debug.Log(selectedSlot);

        Item item = piece.GetComponent<ItemWrapper>().item;
        Vector2Int itemCenter = GetCenterItem(item);
        Debug.Log(itemCenter);

        List<Slot> newSlots = new List<Slot>();
        for (int y = 0; y < item.height; y++)
        {
            for (int x = 0; x < item.width; x++)
            {
                int indexX = selectedSlot.x + (x - (int)item.width / 2);
                int indexY = selectedSlot.y + (y - (int)item.height / 2);
                if (CheckAvailabilityAnimal(item, x, y))
                {
                    if (CheckForAvailability(indexX, indexY))
                    {
                        newSlots.Add(slots[indexX, indexY]);
                    }
                    else
                    {
                        Debug.Log("no fit marh dewd");
                        return;
                    }
                }
            }
        }

        foreach (Slot s in newSlots)
        {
            s.occupied = true;
            s.item = item;
            s.transform.GetComponent<Image>().color = Color.red;
        }
        newSlots.Clear();
    }
    private bool CheckAvailabilityAnimal(Item i, int x, int y)
    {
        if (i.shapeArray[x + y * i.width])
        {
            Debug.Log($"Succ animal grid : {x},{y}");
            return true;
        }
        else
        {
            Debug.Log($"Oof animal grid : {x},{y} width: {i.width}; Using: {!i.shapeArray[x + y * i.width]}");
            return false;
        }
    }
    private bool CheckForAvailability(int x, int y)
    {
        if (x < 0 || x >= size.x || y < 0 || y >= size.y || slots[x, y].occupied)
        {
            Debug.Log($"slot : {x},{y}");
            return false;
        }
        else
        {
            Debug.Log($"slot : {x},{y}");
            return true;
        }
    }


    private Vector2Int GetSlot(PointerEventData ped)
    {
        RectTransform rt = transform as RectTransform;
        Vector2 localPos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, ped.position, ped.pressEventCamera, out localPos);

        localPos = (localPos + new Vector2(rt.sizeDelta.x / 2, -rt.sizeDelta.y / 2)) * new Vector2(1, -1);

        Vector2 selectedSlotCoords = (localPos * size) / (rt.sizeDelta);
        return new Vector2Int((int)Mathf.Floor(selectedSlotCoords.x), (int)Mathf.Floor(selectedSlotCoords.y));
    }

    public Vector2Int GetCenterItem(Item item)
    {
        int x, y;

        if (item.width % 2 == 1)
            x = (int)item.width - 1 / 2;
        else x = (int)item.width / 2;

        if (item.height % 2 == 1)
            y = (int)item.height + 1 / 2;
        else y = (int)item.height / 2;

        return new Vector2Int(x,y);
    }
}
