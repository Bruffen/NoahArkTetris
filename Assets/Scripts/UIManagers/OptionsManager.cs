using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public Button BackButton;

    private void Awake() {
        BackButton.onClick.AddListener(() => {
            CanvasManager.Instance.SetState(GameState.Menu);
        });
    }
}
