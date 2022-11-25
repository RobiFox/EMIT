using UnityEngine;

namespace Level_Specific {
    public class PreEpilogue : MonoBehaviour {
        private bool _changeVolume;
        private float _vel;
    
        public float targetVolume;

        public bool ChangeVolume {
            get => _changeVolume;
            set {
                _changeVolume = value;
                targetVolume = value ? 0f : SettingsManager.instance.MasterVolume;
            }
        }

        void Start() {
            _changeVolume = false;
        }

        public void SetChangeVolume() {
            ChangeVolume = true;
        }
        
        void Update() {
            if (ChangeVolume) {
                AudioListener.volume = Mathf.SmoothDamp(AudioListener.volume, targetVolume, ref _vel, 1f);
            }
        }
    }
}
