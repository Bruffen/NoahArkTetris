using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Ship
{
    public static class ShipManager
    {
        public static Ladder[] Ladders;
        public static int LadderCooldownTime;
        public static BoxCollider2D Box;
        public static float FloorHeight;
        public static float FloorsNumber;
        public static float FinalRotation;
        public static Vector2[] FloorPositions;

        public static void SetLadderClimbing(int index)
        {
            Ladders[index].InUse = true;
            new Thread(() => LadderCooldown(index)).Start();
        }

        public static void LadderCooldown(int index)
        {
            Thread.Sleep(LadderCooldownTime);
            Ladders[index].InUse = false;
        }
    }

    [Serializable]
    public struct Ladder
    {
        public Vector2 Position;
        public bool InUse;
    }
}
