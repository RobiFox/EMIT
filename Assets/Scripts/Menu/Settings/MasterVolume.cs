using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Settings {
    public class MasterVolume : MonoBehaviour {
        private Slider _slider;
        public TextMeshProUGUI valueText;

        void Start() {
            _slider = GetComponent<Slider>();
            _slider.SetValueWithoutNotify(SettingsManager.instance.MasterVolume);
            _slider.onValueChanged.AddListener(SetMasterVolume);

            valueText.text = _slider.value.ToString("0.00");
        }

        public void SetMasterVolume(float arg0) {
            SettingsManager.instance.MasterVolume = arg0;
            valueText.text = _slider.value.ToString("0.00");
        }
    }
}
