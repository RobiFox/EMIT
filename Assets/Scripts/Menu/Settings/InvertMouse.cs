using UnityEngine;
using UnityEngine.UI;

namespace Menu.Settings {
    public class InvertMouse : MonoBehaviour {
        public bool x;
        private Toggle _toggle;

        void Awake() {
            _toggle = GetComponent<Toggle>();
            _toggle.SetIsOnWithoutNotify(x ? SettingsManager.instance.InvertX : SettingsManager.instance.InvertY);
            _toggle.onValueChanged.AddListener(ChangeSensitivity);
        }

        private void ChangeSensitivity(bool b) {
            if (x) SettingsManager.instance.InvertX = b;
            else SettingsManager.instance.InvertY = b;
        }
    }
}
