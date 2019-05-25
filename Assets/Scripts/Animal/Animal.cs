using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class Animal : ScriptableObject {
    public AnimalType animalType;
    public RuntimeAnimatorController boatAnimator;
    [Range(0.1f, 5f)]
    public float speedMultiplier = 1.0f;
    public Sprite tetrisSprite;
    public Color tetrisColor;
    public int scoreValue;

    [HideInInspector]
    public bool[] shapeArray;
    public uint height;
    public uint width;
}
