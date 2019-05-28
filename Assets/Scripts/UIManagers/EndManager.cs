using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndManager : MonoBehaviour
{
    public Button FinishButton;

    private void Awake()
    {
        FinishButton.onClick.AddListener(()=>{
            CanvasManager.Instance.SetState(GameState.Menu);
            AudioManager.Instance.Stop("EndGame");
        });
    }
}
