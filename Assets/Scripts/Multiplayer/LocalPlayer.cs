using Mirror;
using UnityEngine;

namespace Multiplayer {
    public class LocalPlayer : NetworkBehaviour {
        void Start() {
            if (!isLocalPlayer) {
                Destroy(this);
                return;
            }
            FindObjectOfType<MpGameManager>().Setup();
            FindObjectOfType<MpPauseMenu>().SetMultiplayerPlayerValues();
        }
    }
}