using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Settings {
    public class ResolutionDropdown : MonoBehaviour {
        private TMP_Dropdown _dropdown;
        public TMP_Dropdown screenType;
        public TextMeshProUGUI debug;
        void Start() {
            _dropdown = GetComponent<TMP_Dropdown>();
            RefreshResolutions();
        }

        private void RefreshResolutions() {
            _dropdown.options.Clear();
            foreach (Resolution res in Screen.resolutions) {
                _dropdown.options.Add(new TMP_Dropdown.OptionData(res.ToString()));
            }
            var resolution = Screen.fullScreenMode == FullScreenMode.Windowed ? new Resolution {width = Screen.width, height = Screen.height, refreshRate = Screen.currentResolution.refreshRate} : Screen.currentResolution;
            
            _dropdown.SetValueWithoutNotify(Array.IndexOf(Screen.resolutions, resolution));
            //_dropdown.SetValueWithoutNotify(0);
        }

        public void SetResolution() {
            Resolution resolution = Screen.resolutions[_dropdown.value];
            Screen.SetResolution(resolution.width, resolution.height, GetScreenMode(), resolution.refreshRate);
            //RefreshResolutions();
        }

        private FullScreenMode GetScreenMode() {
            switch (screenType.value) {
                case 0:
                    return FullScreenMode.FullScreenWindow;
                case 1:
                    return FullScreenMode.MaximizedWindow;
                default:
                    return FullScreenMode.Windowed;
            }
        }
    }
}
