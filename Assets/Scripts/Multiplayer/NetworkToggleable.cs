using Mirror;

namespace Multiplayer {
    public class NetworkToggleable : NetworkBehaviour {
        [SyncVar] public bool status;
    }
}
