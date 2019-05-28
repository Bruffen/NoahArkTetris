using System;
using System.Collections;
using System.Collections.Generic;
using Ship;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float MatchTime;
    private float timeLeft;
    private Action GameStateUpdate;
    private static int playerScore;
    private static int animalsSaved;
    public GameObject boat;
    public ShipMovement left;
    public ShipMovement right;
    public GameObject water;
    public GameObject background;
    private Animator boatAnimator;
    public BoatSpawner boatSpawner;

    private GameObject oldBoat;

    public TextMeshProUGUI score;
    public TextMeshProUGUI animalCount;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        AudioManager.Instance.Play("Jogo");
    }

    void Update()
    {
        GameStateUpdate?.Invoke();
    }

    public void GameStart()
    {
        if (oldBoat != null)
        {
            Destroy(oldBoat);
        }
        timeLeft = MatchTime;
        playerScore = 0;
        animalsSaved = 0;
        GameStateUpdate += GameRunningUpdate;
        water.SetActive(true);
        background.SetActive(true);
        oldBoat = Instantiate(boat, boat.transform.position, Quaternion.identity, this.transform);
        boatAnimator = oldBoat.GetComponent<Animator>();

        boatSpawner.ResetValues();
    }

    private void GameRunningUpdate()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            Debug.Log("TIMES UP");
            boatAnimator.SetTrigger("end");
            GameEndUpdate();
            GameStateUpdate -= GameRunningUpdate;
        }

        //no more boats coming
        /* if(boatSpawner.totalSpawnedBoats >= boatSpawner.MaxBoatsToSpawn && BoatSpawner.activeBoatNum <= 0)
        {
            Debug.Log("No More Boats");
            boatAnimator.SetTrigger("end");
        }*/
    }
    private void GameEndUpdate()
    {
        AudioManager.Instance.Play("EndGame");
        CanvasManager.Instance.SetState(GameState.End);
        score.text = playerScore.ToString();
        animalCount.text = animalsSaved.ToString();
    }

    public void AddScore(int animalValue)
    {
        playerScore += animalValue;
        animalsSaved++;
    }
}
