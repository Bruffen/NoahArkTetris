using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    void Start()
    {
        var rect = this.GetComponent<RectTransform>().rect;
        bounds = new Vector2(rect.width, rect.height);

        sWidth = rect.width / size.x;
        sHeight = rect.height / size.y;
        grid = GetComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(sWidth, sHeight);

        slots = new Slot[size.x, size.y];
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector2 pos = new Vector2((rect.position.x - bounds.x / 2) + sWidth * x, (rect.position.y + bounds.y / 2) + sHeight * y);
                GameObject s = Instantiate(slotPrefab, pos, Quaternion.identity, this.transform);
            }
        }
    }

}
