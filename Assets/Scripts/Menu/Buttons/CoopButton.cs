using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu.Buttons {
    public class CoopButton : MonoBehaviour {
        // TODO update
        public void PressButton() {
            SceneManager.LoadScene("Network Menu");
        }
    }
}
