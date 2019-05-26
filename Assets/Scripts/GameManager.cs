using System;
using System.Collections;
using System.Collections.Generic;
using Ship;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float MatchTime;
    private float timeLeft;
    private Action GameStateUpdate;
    private static int playerScore;

    public GameObject boat;
    public ShipMovement left;
    public ShipMovement right;
    public GameObject water;
    public GameObject background;
    private Animator animator;

    private GameObject oldBoat;

    void Awake()
    {
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(this.gameObject);
        }
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        AudioManager.Instance.Play("Music");
    }

    void Update()
    {
        GameStateUpdate?.Invoke();
    }

    public void GameStart(){
        if(oldBoat != null){
            Destroy(oldBoat);
        }
        timeLeft = MatchTime;
        playerScore = 0;
        GameStateUpdate += GameRunningUpdate;
        water.SetActive(true);
        background.SetActive(true);
        oldBoat = Instantiate(boat, boat.transform.position, Quaternion.identity, this.transform);
    }

    private void GameRunningUpdate(){
        timeLeft -= Time.deltaTime;

        if(timeLeft <= 0){
            Debug.Log("TIMES UP");
            animator.SetTrigger("end");
        }
    }
    private void GameEndUpdate(){
            CanvasManager.Instance.SetState(GameState.End);
    }

    public void AddScore(int animalValue){
        playerScore += animalValue;
    }
}
