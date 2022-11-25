using Mirror;
using UnityEngine;

namespace Multiplayer {
    public class NetworkLevelItem : NetworkBehaviour {
        public Object[] destroyForOthers;
        public bool ownerIsHost;

        void Start() {
            transform.SetParent(null);
            if(isServer) {
                NetworkServer.Spawn(gameObject);
            }

            if (isServer != ownerIsHost) {
                foreach (Object o in destroyForOthers) {
                    Destroy(o);
                }
            }
        }
    }
}
