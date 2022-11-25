using System;
using UnityEngine;

namespace Mechanics {
    public class TimelessZone : MonoBehaviour {
        public GameObject timelessZoneSound;
        private void OnTriggerEnter(Collider other) {
            Instantiate(timelessZoneSound, other.transform.position, Quaternion.identity);
            if (other.GetComponent<PlayerMove>() != null) {
                FindObjectOfType<GameManager>().RestartGame();
            } else if (other.GetComponent<GrabbableCube>() != null) {
                GetComponent<TimelessNotificator>().SendTutorial(other.gameObject);
            }
        }
    }
}
