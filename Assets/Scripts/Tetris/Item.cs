using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject {
    [HideInInspector]
    public bool[] shapeArray;
    public uint height;
    public uint width;
}
