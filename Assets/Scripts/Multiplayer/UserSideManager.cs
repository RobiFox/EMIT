using UnityEngine;
using Mirror;

namespace Multiplayer {
    public class UserSideManager : NetworkBehaviour {
        public Object[] onlyClient;
        public Object[] onlyOthers;
        
        public GameObject view;
        public GameObject head;

        void Start() {
            if (isLocalPlayer) {
                GetComponent<Animator>().SetLayerWeight(1, 1f);
                foreach(Object o in onlyOthers) {
                    Destroy(o);
                }

                GameManager.Player = gameObject;
            } else {
                foreach(Object o in onlyClient) {
                    Destroy(o);
                }
            }
        }

        void Update() {
            if (!isLocalPlayer) return;
            head.transform.rotation = view.transform.rotation;
        }
    }
}
