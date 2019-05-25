using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Menu,
    Ingame,
    End,
    Options,
}
public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    public RectTransform Menu;
    public RectTransform Game;
    public RectTransform End;
    public RectTransform Options;

    private GameState currentState;
    private RectTransform currentTransform;

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

        currentState = GameState.Menu;
        currentTransform = Menu;
    }

    public void SetState(GameState newState)
    {
        currentTransform.gameObject.SetActive(false);

        switch (newState)
        {
            case GameState.Menu:
                currentTransform = Menu;
                break;
            case GameState.Ingame:
                currentTransform = Game;
                GameManager.Instance.GameStart();
                break;
            case GameState.End:
                currentTransform = End;
                break;
            case GameState.Options:
                currentTransform = Options;
                break;
        }

        currentTransform.gameObject.SetActive(true);
    }
}
