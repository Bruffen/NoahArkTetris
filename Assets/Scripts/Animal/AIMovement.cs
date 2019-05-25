﻿using System.Collections;
using System.Collections.Generic;
using Ship;
using UnityEngine;

namespace Mob
{
    public class AIMovement : MonoBehaviour
    {
        public float MovementSpeed = 5.0f;
        public State CurrentState;
        public int currentFloor;

        private Transform transformBoat;
        private float boatAngle;
        private float fallingDirection = 0.0f;
        private float fallingSpeed = 2.0f;
        private float timeToWait;
        private float stopTimer;
        private float seekTimer;

        void Start()
        {
            CurrentState = State.Moving;
            transformBoat = transform.parent.transform;
            currentFloor = Random.Range(0, ShipManager.Ladders.Length + 1);
            transform.localPosition = new Vector2(
                //TODO 0.5f replace with animal size
                Random.Range(-(ShipManager.Size.x / 2.0f) + 0.5f, (ShipManager.Size.x / 2.0f) - 0.5f),
                -(ShipManager.Size.y / 2.0f) + 0.5f + currentFloor * ShipManager.FloorHeight);
            stopTimer = 0.0f;
            seekTimer = 0.0f;
        }

        void Update()
        {
            boatAngle = transformBoat.rotation.eulerAngles.z;
            if (boatAngle > 180)
                boatAngle = transformBoat.rotation.eulerAngles.z - 360.0f;

            switch (CurrentState)
            {
                case State.Moving:
                    Movement();
                    CheckForStop();
                    CheckForFall();
                    CheckForSeek();
                    break;
                case State.Stopped:
                    Stop();
                    CheckForFall();
                    CheckForSeek();
                    break;
                case State.Seeking:
                    Seek();
                    CheckForFall();
                    break;
                case State.Waiting:
                    Wait();
                    CheckForFall();
                    break;
                case State.Climbing:
                    Climb();
                    break;
                case State.Falling:
                    Fall();
                    break;
            }
        }

        /**
         * Normal moving behaviour
         */
        void Movement()
        {
            float bound = ShipManager.Size.x / 2.0f;

            //Clamp to range [0, MovementSpeed]
            float boatAngleClamped = Mathf.Clamp(Mathf.Abs(boatAngle), 0.0f, 90.0f) / (90.0f / MovementSpeed);
            transform.localPosition += new Vector3(Time.deltaTime * (MovementSpeed - boatAngleClamped), 0.0f, 0.0f);

            if (transform.localPosition.x > bound || transform.localPosition.x < -bound)
            {
                MovementSpeed = -MovementSpeed;
                transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -bound, bound), transform.localPosition.y);
            }
        }

        /**
         * Slip down the ship
         */
        void Fall()
        {
            float killingAngle = ShipManager.FinalRotation;
            float boatAngleClamped = (Mathf.Clamp(Mathf.Abs(boatAngle), killingAngle, 90.0f) - killingAngle) / ((90.0f - killingAngle) / Mathf.Abs(MovementSpeed));

            MovementSpeed += (Time.deltaTime * fallingDirection) * boatAngleClamped;
            MovementSpeed *= (1.0f + fallingSpeed * Time.deltaTime);
            transform.localPosition += new Vector3(Time.deltaTime * MovementSpeed, 0.0f);
        }

        /**
         * Stop and idle for a random amount of time
         */
        void Stop()
        {
            timeToWait -= Time.deltaTime;
            if (timeToWait < 0.0f)
            {
                CurrentState = State.Moving;
                if (Random.Range(0, 2) == 1)
                    MovementSpeed = -MovementSpeed;
            }
        }

        /**
         * Seek the floor's ladder to climb
         */
        void Seek()
        {
            if (currentFloor == ShipManager.Ladders.Length)
            {
                CurrentState = State.Moving;
                return;
            }

            MovementSpeed = Mathf.Abs(MovementSpeed);
            Ladder ladder = ShipManager.Ladders[currentFloor];
            Vector2 target = ladder.Position;
            float direction = Mathf.Sign(target.x - transform.localPosition.x);
            float boatAngleClamped = Mathf.Clamp(Mathf.Abs(boatAngle), 0.0f, 90.0f) / (90.0f / MovementSpeed);
            transform.localPosition += new Vector3(Time.deltaTime * (MovementSpeed - boatAngleClamped), 0.0f) * direction;

            if (Mathf.Abs(target.x - transform.localPosition.x) < 0.1f)
            {
                if (ladder.InUse)
                {
                    CurrentState = State.Waiting;
                }
                else
                {
                    CurrentState = State.Climbing;
                    ShipManager.SetLadderClimbing(currentFloor);
                    currentFloor++;
                }
            }
        }

        /**
         * Wait for the ladder to be available
         */
        void Wait()
        {
            if (!ShipManager.Ladders[currentFloor].InUse)
            {
                ShipManager.SetLadderClimbing(currentFloor);
                CurrentState = State.Climbing;
            }
        }

        /** 
         * Climb the ladder to the next floor
         */
        void Climb()
        {
            transform.localPosition += Vector3.up * Time.deltaTime * 2.0f;
            if (transform.localPosition.y > -(ShipManager.Size.y / 2.0f) + 0.5f + currentFloor * ShipManager.FloorHeight)
            {
                CurrentState = State.Moving;
            }
        }

        void CheckForFall()
        {
            Debug.Log(ShipManager.FinalRotation);
            if (boatAngle >= ShipManager.FinalRotation)
            {
                fallingDirection = -1.0f;
                MovementSpeed = -0.1f;
                CurrentState = State.Falling;
            }
            else if (boatAngle <= -ShipManager.FinalRotation)
            {
                fallingDirection = 1.0f;
                MovementSpeed = 0.1f;
                CurrentState = State.Falling;
            }
        }

        void CheckForStop()
        {
            stopTimer += Time.deltaTime;
            if (stopTimer < 1.0f)
                return;

            stopTimer = 0.0f;

            if (Random.Range(0.0f, 1.0f) > 0.7f)
            {
                timeToWait = Random.Range(0.5f, 3.0f);
                CurrentState = State.Stopped;
            }
        }

        void CheckForSeek()
        {
            seekTimer += Time.deltaTime;
            if (seekTimer < 1.0f)
                return;

            seekTimer = 0.0f;

            if (Random.Range(0.0f, 1.0f) > 0.85f)
            {
                CurrentState = State.Seeking;
            }
        }
    }

    public enum State
    {
        Stopped,
        Moving,
        Waiting,
        Climbing,
        Falling,
        Seeking
    }
}
