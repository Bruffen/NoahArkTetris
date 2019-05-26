using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ship
{
    public class ShipManagerWrapper : MonoBehaviour
    {
        public Transform[] LadderPositions;
        public Transform[] FloorPositions;
        public int LadderCooldown = 500;
        public Vector2 Size;
        public int FloorsNumber;
        public float FinalRotation = 70.0f;
        public float FloorHeight = 1.735f;

        public Ladder[] ladders;
        public Vector2[] floorPositions;

        void Start()
        {
            ladders = new Ladder[LadderPositions.Length];
            for (int i = 0; i < ladders.Length; i++)
            {
                ladders[i].Position = LadderPositions[i].localPosition;
                ladders[i].InUse = false;
            }

            floorPositions = new Vector2[FloorPositions.Length];
            for (int i = 0; i < floorPositions.Length; i++)
            {
                floorPositions[i] = FloorPositions[i].localPosition;
            }

            ShipManager.Ladders = ladders;
            ShipManager.FloorPositions = floorPositions;
            ShipManager.Box = GetComponent<BoxCollider2D>();
            ShipManager.LadderCooldownTime = LadderCooldown;
            ShipManager.FloorsNumber = FloorsNumber;
            ShipManager.FloorHeight = FloorHeight;
            ShipManager.FinalRotation = FinalRotation;
            if (FloorsNumber == 0) Debug.LogError("Set number of floors!");
        }
    }
}