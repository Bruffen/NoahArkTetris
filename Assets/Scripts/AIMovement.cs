using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public Vector2[] Ladders;
    public float MovementSpeed = 5.0f;

    private Transform transformBoat;
    private int currentFloor;
    private bool isFalling;
    private float fallingDirection = 0.0f;

    void Start()
    {
        transformBoat = transform.parent.transform;
        currentFloor = Random.Range(0, Ladders.Length + 1);
        isFalling = false;
    }

    void Update()
    {
        if (isFalling)
            Fall();
        else
            Movement();


    }

    void Movement()
    {
        float boatAngle = transformBoat.rotation.eulerAngles.z;
        if (boatAngle > 180)
            boatAngle = transformBoat.rotation.eulerAngles.z - 360.0f;

        //Value of range [0, MovementSpeed]
        float boatAngleClamped = Mathf.Clamp(Mathf.Abs(boatAngle), 0.0f, 90.0f) / (90.0f / MovementSpeed);
        transform.localPosition += new Vector3(Time.deltaTime * (MovementSpeed - boatAngleClamped), 0.0f, 0.0f);

        if (transform.localPosition.x > 5.0f || transform.localPosition.x < -5.0f)
            MovementSpeed = -MovementSpeed;

        if (boatAngle > 65.0f)
        {
            Debug.Log(transformBoat.rotation.eulerAngles.z);
            fallingDirection = -1.0f;
            MovementSpeed = fallingDirection;
            isFalling = true;
            Debug.Log("Falling left");
        }
        else if (boatAngle < -65.0f)
        {
            fallingDirection = 1.0f;
            MovementSpeed = fallingDirection;
            isFalling = true;
            Debug.Log("Falling Right");
        }
    }

    void Fall()
    {
        MovementSpeed += Time.deltaTime * 2.0f * fallingDirection;
        MovementSpeed *= (1.0f + Time.deltaTime);
        transform.localPosition += new Vector3(Time.deltaTime * (MovementSpeed), 0.0f, 0.0f);
    }
}
