using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class FunctionCollection : MonoBehaviour {
    public void Inactive() {
        gameObject.SetActive(false);
    }

    public void DisablePlayerMovement() {
        TogglePlayerMovement(false);
    }
    
    public void EnablePlayerMovement() {
        TogglePlayerMovement(true);
    }

    public void TogglePlayerMovement(bool toggle) {
        FindObjectOfType<PlayerMove>().enable = toggle;
        FindObjectOfType<PlayerLook>().enable = toggle;
    }
}
