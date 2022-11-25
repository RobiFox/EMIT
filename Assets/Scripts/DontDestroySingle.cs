using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DontDestroySingle : MonoBehaviour {
    void Start() {
        if(FindObjectsOfType<DontDestroySingle>().Length > 1) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
