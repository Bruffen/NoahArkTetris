using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoatSpawner : MonoBehaviour
{
    public GameObject boatPrefab;
    public GameObject boatCountUI;

    //boat spawner limiter
    public int MaxBoatsToSpawn = 5;
    public int totalSpawnedBoats;
    public static int activeBoatNum;

    //Boat spawn timer
    private float boatTimer;
    public float boatSpawnTime = 4;

    private void Start()
    {
        activeBoatNum = 0;
        totalSpawnedBoats = 0;
    }

    private void Update()
    {
        if(totalSpawnedBoats < MaxBoatsToSpawn)
        {
            if (activeBoatNum < 4)
            {
                if (boatTimer < boatSpawnTime)
                    boatTimer += Time.deltaTime;
                else
                {
                    SpawnBoat();
                    boatTimer = 0f;
                }
            }
        }
    }

    private void SpawnBoat()
    {
        GameObject obj = Instantiate(boatPrefab, this.transform);
        activeBoatNum++;
        totalSpawnedBoats ++;

        UpdateBoatCountUI();
    }

    private void UpdateBoatCountUI()
    {
        boatCountUI.GetComponent<TextMeshProUGUI>().text = (MaxBoatsToSpawn - totalSpawnedBoats).ToString() + " Left";
    }
}
