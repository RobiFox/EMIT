using Mechanics;
using Player;
using UnityEngine;

namespace Multiplayer {
    public class MpObjectGrabber : ObjectGrabber {
        public NetworkObjectGrabber networkObjectGrabber;

        public GrabbableCube grabbed {
            get { return base.grabbed; }
            set {
                //networkObjectGrabber.grabbed = value.gameObject;
                networkObjectGrabber.CmdSetCubeStatus(value.gameObject, true);
                base.grabbed = value;
            }
        }

        public new void Start() {
            if (!networkObjectGrabber.isLocalPlayer) return;
            base.Start();
        }
        
        public new void Update() {
            if (!networkObjectGrabber.isLocalPlayer) return;
            base.Update();
        }

        public override void GrabCube(RaycastHit hit) {
            base.GrabCube(hit);
            if (hit.transform.GetComponent<GrabbableCube>() != null && hit.transform.GetComponent<GrabbableCube>().IsAvailableForGrab()) {
                networkObjectGrabber.CmdSetCubeStatus(hit.collider.gameObject, true);
            }
        }

        public override void DropCube() {
            networkObjectGrabber.CmdSetCubeStatus(Grabbed.gameObject, false);
            ApplyTransformForCube(Grabbed.gameObject);
            base.DropCube();
        }

        public new void ApplyTransformForCube(GameObject go) {
            var (vector3, quaternion) = GetTransformForCube(go);
            networkObjectGrabber.CmdDropBlock(vector3, quaternion);
        }
    }
}
