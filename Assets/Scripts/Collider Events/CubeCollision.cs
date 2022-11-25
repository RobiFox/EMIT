using System;
using System.Collections;
using System.Collections.Generic;
using Mechanics;
using UnityEngine;
using UnityEngine.Events;

public class CubeCollision : MonoBehaviour {
    [Serializable] public class CollisionEvent : UnityEvent { }
    
    [SerializeField] private CollisionEvent onTriggerEnter = new CollisionEvent();

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<GrabbableCube>() != null) {
            onTriggerEnter.Invoke();
        }
    }

    public void GoDestroy(GameObject gameObject) {
        Destroy(gameObject);
    }
    
    public void GoDestroy() {
        GoDestroy(gameObject);
    }
}
