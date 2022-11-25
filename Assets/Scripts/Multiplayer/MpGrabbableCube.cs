using Mechanics;
using UnityEngine;

namespace Multiplayer {
    public class MpGrabbableCube : GrabbableCube {
        public bool isForHost;
        private LocalPlayer _player;
        
        public new void Start() {
            base.Start();
        }

        /*public new void Update() {
            base.Update();
        }*/
        
        public override bool IsAvailableForGrab() {
            if (_player == null) {
                if((_player = FindObjectOfType<LocalPlayer>()) != null)
                    if(!IsForThisPlayer()) {
                        Target = def = 0.3f;
                    }
                return false;
            }
            return IsForThisPlayer();
        }

        private bool IsForThisPlayer() {
            return _player.isServer && isForHost || !_player.isServer && !isForHost;
        }
    }
}