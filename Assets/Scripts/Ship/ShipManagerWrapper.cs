using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ship
{
    public class ShipManagerWrapper : MonoBehaviour
    {
        public Transform[] LadderPositions;
        public int LadderCooldown = 500;
        public Vector2 Size;
        public int FloorsNumber;
        public float FinalRotation = 70.0f;

        public Ladder[] ladders;

        void Start()
        {
            ladders = new Ladder[LadderPositions.Length];
            for (int i = 0; i < ladders.Length; i++)
            {
                ladders[i].Position = LadderPositions[i].localPosition;
                ladders[i].InUse = false;
            }

            ShipManager.Ladders = ladders;
            ShipManager.Size = Size;
            ShipManager.LadderCooldownTime = LadderCooldown;
            ShipManager.FloorsNumber = FloorsNumber;
            ShipManager.FloorHeight = Size.y / (float)FloorsNumber;
            ShipManager.FinalRotation = FinalRotation;
            if (FloorsNumber == 0) Debug.LogError("Set number of floors!");
        }
    }
}