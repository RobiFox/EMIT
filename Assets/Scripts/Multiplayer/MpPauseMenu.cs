using Menu;
using Mirror;
using UnityEngine;

namespace Multiplayer {
    public class MpPauseMenu : PauseMenu {
        public GameObject[] toRemove;

        public new void Start() {
            base.Start();
            foreach (GameObject go in toRemove) {
                Destroy(go.gameObject);
            }
        }
        
        public override void FreezeTime() {
            
        }
        
        public override void ReturnTime() {
            
        }

        public override void SetPlayerValues() {
            
        }
        
        public void SetMultiplayerPlayerValues() {
            base.SetPlayerValues();
        }
        
        public new void GoToMainMenu() {
            Time.timeScale = 1f;
            NetworkManager.singleton.StopClient();
        }
    }
}