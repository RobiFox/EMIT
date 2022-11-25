using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Settings {
    public class AmbientVolume : MonoBehaviour {
        private Slider _slider;
        public TextMeshProUGUI valueText;

        void Start() {
            _slider = GetComponent<Slider>();
            _slider.SetValueWithoutNotify(SettingsManager.instance.AmbientVolume);
            _slider.onValueChanged.AddListener(SetAmbientVolume);

            valueText.text = _slider.value.ToString("0.00");
        }

        public void SetAmbientVolume(float arg0) {
            SettingsManager.instance.AmbientVolume = arg0;
            valueText.text = _slider.value.ToString("0.00");
        }
    }
}
