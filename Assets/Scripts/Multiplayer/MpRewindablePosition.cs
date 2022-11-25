using System.Collections.Generic;
using Mechanics.Time_Mechanic;
using Mirror;
using UnityEngine;

namespace Multiplayer {
    public class MpRewindablePosition : RewindablePosition {
        protected override void Rewind() {
            base.Rewind();
            FindObjectOfType<NetworkObjectGrabber>().CmdSetObjectLocation(gameObject, transform.position, transform.rotation);
        }

        public new void OnRewindStart() {
            FindObjectOfType<NetworkObjectGrabber>().CmdFreezeGameObject(gameObject, true);
        }
        
        public new void OnRewindEnd() {
            FindObjectOfType<NetworkObjectGrabber>().CmdFreezeGameObject(gameObject, false);
        }
    }
}