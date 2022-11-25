using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Settings {
    public class CubeDropdown : MonoBehaviour {
        private TMP_Dropdown _dropdown;

        private void Start() {
            _dropdown = GetComponent<TMP_Dropdown>();
            _dropdown.SetValueWithoutNotify(SettingsManager.instance.ShowCube);
            _dropdown.onValueChanged.AddListener(SetValue);
        }

        public void SetValue(int val) {
            SettingsManager.instance.ShowCube = val;
        }
    }
}
