using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Settings {
    public class MouseSensitivity : MonoBehaviour {
        private Slider _slider;
        public TextMeshProUGUI valueText;

        void Start() {
            _slider = GetComponent<Slider>();
            _slider.SetValueWithoutNotify(SettingsManager.instance.MouseSensitivity);
            _slider.onValueChanged.AddListener(SetMouseSensitivity);

            valueText.text = _slider.value.ToString("0.00");
        }

        public void SetMouseSensitivity(float arg0) {
            SettingsManager.instance.MouseSensitivity = arg0;
            valueText.text = _slider.value.ToString("0.00");
        }
    }
}
