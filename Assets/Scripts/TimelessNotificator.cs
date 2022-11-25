using System.Collections;
using System.Collections.Generic;
using Mechanics.Time_Mechanic;
using UnityEngine;

public class TimelessNotificator : MonoBehaviour {
    private CanvasManager _canvasManager;
    private GameManager _gameManager;

    void Start() {
        _canvasManager = FindObjectOfType<CanvasManager>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void SendTutorial(GameObject go) {
        if (_gameManager.canRewind && go.GetComponent<RewindablePosition>() != null) {
            _canvasManager.SetTutorialRewind();
        } else {
            _canvasManager.SetTutorialRestart();
        }
    }
}
