using UnityEngine;

namespace Multiplayer {
    public class DoubleMovingPlatform : MonoBehaviour {
        private Vector3 position;
        void Start() {
            foreach(Transform t in transform) {
                t.GetComponent<MpMovingPlatform>().Target = position;
            }
        }
    }
}