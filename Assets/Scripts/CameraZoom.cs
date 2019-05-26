﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float MinZoom;
    public float MaxZoom;
    public float MouseSensitivity = 20.0f;

    private Camera cam;
    private Vector2 botLeftCorner;
    private Vector2 topRightCorner;

    void Start()
    {
        cam = Camera.main;
        botLeftCorner = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        topRightCorner = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            ZoomOrthoCamera(cam.ScreenToWorldPoint(Input.mousePosition), Time.deltaTime * MouseSensitivity);

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            ZoomOrthoCamera(cam.ScreenToWorldPoint(Input.mousePosition), -Time.deltaTime * MouseSensitivity);
    }

    void ZoomOrthoCamera(Vector3 zoomTowards, float amount)
    {
        /*if ((cam.orthographicSize >= MaxZoom && amount < 0.0f) || (cam.orthographicSize <= MinZoom && amount > 0.0f))
            return;*/

        float multiplier = (1.0f / cam.orthographicSize * amount);

        //Vector2 excess = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, cam.nearClipPlane)) - cam.transform.position;
        Vector2 excess = new Vector2(cam.orthographicSize * Screen.width / Screen.height, cam.orthographicSize);

        cam.transform.position += (zoomTowards - transform.position) * multiplier;
        cam.transform.position = new Vector3(
            Mathf.Clamp(cam.transform.position.x, botLeftCorner.x + excess.x, topRightCorner.x - excess.x),
            Mathf.Clamp(cam.transform.position.y, botLeftCorner.y + excess.y, topRightCorner.y - excess.y),
            cam.transform.position.z
        );

        cam.orthographicSize -= amount;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, MinZoom, MaxZoom);
    }

    void DragCamera()
    {

    }
}