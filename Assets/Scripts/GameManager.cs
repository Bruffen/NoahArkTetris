using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float MatchTime;
    private float timeLeft;
    private Action GameStateUpdate;
    private static int playerScore;

    void Awake()
    {
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(this.gameObject);
        }
    }
    void OnEnable()
    {
        GameStart();        
    }

    void Update()
    {
        GameStateUpdate?.Invoke();
    }

    public void GameStart(){
        timeLeft = MatchTime;
        playerScore = 0;
        GameStateUpdate += GameRunningUpdate;
    }

    private void GameRunningUpdate(){
        timeLeft -= Time.deltaTime;

        if(timeLeft <= 0){
            Debug.Log("TIMES UP");
            CanvasManager.Instance.SetState(GameState.End);
        }
    }
    private void GameEndUpdate(){

    }

    public void AddScore(int animalValue){
        playerScore += animalValue;
    }
}
