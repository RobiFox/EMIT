using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Settings {
    public class CrosshairDropdown : MonoBehaviour {
        private TMP_Dropdown _dropdown;

        private void Start() {
            _dropdown = GetComponent<TMP_Dropdown>();
            _dropdown.SetValueWithoutNotify(SettingsManager.instance.CrosshairType);
            _dropdown.onValueChanged.AddListener(SetValue);
        }

        public void SetValue(int val) {
            SettingsManager.instance.CrosshairType = val;
        }
    }
}
