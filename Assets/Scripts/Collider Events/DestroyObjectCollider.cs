using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectCollider : MonoBehaviour {
    public GameObject[] objects;
    private void OnTriggerEnter(Collider other) {
        foreach (GameObject go in objects) {
            go.SetActive(false);
        }
    }
}
