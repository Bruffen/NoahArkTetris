using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ship
{
    public class ShipMovement : MonoBehaviour
    {
        public float Speed = 2.0f;
        public float FallingSpeed = 0.2f;
        public bool IsRightSide;

        public float initialRotation = 0.0f;
        public float finalRotation;

        private float direction = 1.0f;
        private float duration = 0.0f;
        private float time = 0.0f;

        void Start()
        {
            finalRotation = ShipManager.FinalRotation;
            if (IsRightSide)
            {
                direction = -direction;
                finalRotation = 360.0f - finalRotation;
            }

            duration = GameManager.Instance.MatchTime;
            time = 0.0f;
        }

        void Update()
        {
            time += Time.deltaTime;

            float boatAngle = transform.rotation.eulerAngles.z;
            if (boatAngle > 180)
                boatAngle = transform.rotation.eulerAngles.z - 360.0f;

            transform.position += Time.deltaTime * FallingSpeed * Vector3.down;

            transform.rotation = Quaternion.Lerp(
                Quaternion.Euler(0.0f, 0.0f, initialRotation),
                Quaternion.Euler(0.0f, 0.0f, finalRotation),
                time / duration);
        }
    }
}