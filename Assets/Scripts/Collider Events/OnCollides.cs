using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Collider_Events {
    public class OnCollides : MonoBehaviour {
        [Serializable] public class CollisionEvent : UnityEvent { }
    
        [SerializeField] private CollisionEvent onTriggerEnter = new CollisionEvent();

        private void OnTriggerEnter(Collider other) {
            if (other.GetComponent<PlayerMove>() != null) {
                onTriggerEnter.Invoke();
            }
        }

        public void GoDestroy(GameObject gameObject) {
            Destroy(gameObject);
        }
    
        public void GoDestroy() {
            GoDestroy(gameObject);
        }
    
        public void ChangeScene(string scene) {
            StartCoroutine(ChangeSceneCoroutine(scene));
        }

        private IEnumerator ChangeSceneCoroutine(string scene) {
            FindObjectOfType<CanvasManager>().SetWhite();
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(scene);
        }
    }
}
