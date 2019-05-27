using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoatSpawner : MonoBehaviour
{
    public GameObject boatPrefab;
    //UI
    public GameObject boatCountUI;

    //boat spawner limiter
    public int MaxBoatsToSpawn = 7;
    public int totalSpawnedBoats;
    public static int activeBoatNum;

    //Boat spawn timer
    private float boatTimer;
    public float boatSpawnTime = 4;

    /*  private void Start()
    {
        activeBoatNum = 0;
        totalSpawnedBoats = 0;
        SpawnBoat();
    }*/

    private void Update()
    {
        if(totalSpawnedBoats < MaxBoatsToSpawn)
        {
            if (activeBoatNum < 4)
            {
                Debug.Log("one more");
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

        Debug.Log(activeBoatNum);
    }

    private void UpdateBoatCountUI()
    {
        boatCountUI.GetComponent<TextMeshProUGUI>().text = (MaxBoatsToSpawn - totalSpawnedBoats).ToString() + " Left";
    }

    public void ResetValues()
    {
        Debug.Log("reset alures");
        boatTimer = 0f;
        activeBoatNum = 0;
        totalSpawnedBoats = 0;
        SpawnBoat();
    }
}
