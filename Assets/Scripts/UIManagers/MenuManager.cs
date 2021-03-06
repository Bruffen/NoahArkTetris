﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button StartButton;
    public Button Options;
    public Button EndButton;

    private bool onIntructions = false;
    public GameObject Instructions;
    
    void Awake()
    {
        StartButton.onClick.AddListener(() =>{
            CanvasManager.Instance.SetState(GameState.Ingame);
        });
        Options.onClick.AddListener(() => {
            CanvasManager.Instance.SetState(GameState.Options);
        });
        EndButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }

    public void ActiveIntructions()
    {
        onIntructions = !onIntructions;
        Instructions.SetActive(onIntructions);
    }
}
