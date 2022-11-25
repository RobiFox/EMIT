using Mirror;
using UnityEngine;

namespace Multiplayer {
    public class NetworkGrabbableObject : NetworkBehaviour {
        public Material owner;
        public Material other;

        void Start() {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            MpGrabbableCube cube = GetComponent<MpGrabbableCube>();
            if (cube.isForHost && isServer || !cube.isForHost && !isServer) {
                renderer.material = owner;
            } else {
                renderer.material = other;
                BoxCollider boxCollider = GetComponent<BoxCollider>();
                var size = boxCollider.size;
                size = new Vector3(size.x * 0.9f, size.y * 0.99f, size.z * 0.9f);
                boxCollider.size = size;
            }
        }
    }
}
