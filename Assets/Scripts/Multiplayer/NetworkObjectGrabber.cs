using Mechanics;
using Player;
using UnityEngine;
using Mirror;

namespace Multiplayer {
    public class NetworkObjectGrabber : NetworkBehaviour {
        public ObjectGrabber grabber;
        [SyncVar] public GameObject grabbed;
        
        private Animator _animator;
        private static readonly int HoldingCube = Animator.StringToHash("holdsCube");

        public MeshRenderer thirdPersonGrabbedCube;

        void Start() {
            if (isLocalPlayer) {
                FindObjectOfType<CanvasManager>().ObjectGrabber = grabber;
            }
            _animator = GetComponent<Animator>();
        }
        
        void Update() {
            if (!isLocalPlayer) return;
        }

        [Command]
        public void CmdDropBlock(Vector3 position, Quaternion rotation) {
            RpcSetCubeStatus(grabbed, false);
            grabbed.transform.position = position;
            grabbed.transform.rotation = rotation;
            grabbed.GetComponent<Rigidbody>().velocity = Vector3.zero;
            grabbed.GetComponent<Rigidbody>().AddForce(FindObjectOfType<PlayerMove>().MoveDirection, ForceMode.VelocityChange);
            grabbed = null;
        }
        
        [Command]
        public void CmdFreezeGameObject(GameObject go, bool freeze) {
            go.GetComponent<Rigidbody>().isKinematic = freeze;
        }
        
        [Command]
        public void CmdSetObjectLocation(GameObject gameObject, Vector3 position, Quaternion rotation) {
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;
        }
        
        [Command]
        public void CmdSetStatus(GameObject go, bool toggle) {
            Toggleable toggleable = go.GetComponent<Toggleable>();
            if (toggleable == null) return;
            toggleable.SetStatus(toggle);
        }

        [Command]
        public void CmdSetCubeStatus(GameObject go, bool isGrabbed) {
            grabbed = go;
            RpcSetCubeStatus(go, isGrabbed);
        }

        [ClientRpc]
        public void RpcSetCubeStatus(GameObject go, bool isGrabbed) {
            if(go != null) go.GetComponent<GrabbableCube>().IsGrabbed = isGrabbed;
            if(!isLocalPlayer) {
                _animator.SetBool(HoldingCube, isGrabbed);
                thirdPersonGrabbedCube.enabled = isGrabbed;
            }
        }
    }
}