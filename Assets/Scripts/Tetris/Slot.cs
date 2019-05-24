using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Item item;
    public bool occupied = false;
    public Rect rect;

    /*public void Draw(Vector2 position)
    {
        GUI.DrawTexture(new Rect(position.x + rect.x, position.y + rect.y, rect.width, rect.height) , texture);
    }*/
}
