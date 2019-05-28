using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScroller : MonoBehaviour
{
    public Renderer foregroundWater;
    public Renderer backgroundWater;
    private Vector2 offset;
    public float velocity;

    void Awake()
    {
        offset = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        offset += new Vector2(1, 0) * velocity * Time.deltaTime;
        foregroundWater.material.SetTextureOffset ("_MainTex", offset);
        backgroundWater.material.SetTextureOffset ("_MainTex", offset);
    }
}
