using TMPro;
using UnityEngine;

namespace Menu.Settings {
    public class WindowManager : MonoBehaviour {
        private TMP_Dropdown _dropdown;
        void Start() {
            _dropdown = GetComponent<TMP_Dropdown>();
            _dropdown.SetValueWithoutNotify(Screen.fullScreenMode == FullScreenMode.FullScreenWindow ? 0 : Screen.fullScreenMode == FullScreenMode.MaximizedWindow ? 1 : 2);
        }
    }
}
