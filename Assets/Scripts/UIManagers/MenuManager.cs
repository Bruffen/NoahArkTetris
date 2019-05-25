using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button StartButton;
    public Button Options;
    
    void Awake()
    {
        StartButton.onClick.AddListener(() =>{
            CanvasManager.Instance.SetState(GameState.Ingame);
        });
        Options.onClick.AddListener(() => {
            CanvasManager.Instance.SetState(GameState.Options);
        });
    }
}
