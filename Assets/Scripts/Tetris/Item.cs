using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject {
    public AnimalType animalType;
    public GameObject boatGameObject;
    [Range(0.1f, 5f)]
    public float speedMultiplier = 1.0f;
    public GameObject tetrisGameObject;

    [HideInInspector]
    public bool[] shapeArray;
    public uint height;
    public uint width;
}
