using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ship
{
    public class ShipMovement : MonoBehaviour
    {
        public float Speed = 2.0f;
        public bool IsRightSide;

        private float direction = 1.0f;
        void Start()
        {
            if (IsRightSide) direction = -direction;
        }

        void Update()
        {
            float angle = transform.rotation.eulerAngles.z;
            if (angle > 180)
                angle = transform.rotation.eulerAngles.z - 360.0f;

            if (Mathf.Abs(angle) > 70.0f)
                return;

            transform.Rotate(new Vector3(0.0f, 0.0f, Time.deltaTime * Speed * direction));
        }
    }
}