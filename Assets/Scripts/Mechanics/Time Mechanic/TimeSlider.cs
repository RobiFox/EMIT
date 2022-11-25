using UnityEngine;
using UnityEngine.UI;

namespace Mechanics.Time_Mechanic {
    public class TimeSlider : MonoBehaviour {
        private RewindableObject _ro;
        private GameManager _gm;
        //private Slider _slider;
        private Image _slider;
        private float _vel;
        void Start() {
            _ro = FindObjectOfType<RewindableObject>();
            _gm = FindObjectOfType<GameManager>();
            _slider = GetComponent<Image>();
            _vel = 0;
        }
    
        void Update() {
            if (_gm.IsRewinding() && _ro != null) {
                if (!_ro.IsRewinding) {
                    _ro = FindHighestRewindableObject(true);
                }
                int pos = _ro.PositionCount() - 1;
            
                _slider.fillAmount = Mathf.SmoothDamp(_slider.fillAmount, pos / (30f / Time.fixedDeltaTime), ref _vel, 0.3f, Mathf.Infinity, Time.unscaledDeltaTime);
            } else {
                _slider.fillAmount = Mathf.SmoothDamp(_slider.fillAmount, 0, ref _vel, 0.1f, Mathf.Infinity, Time.unscaledDeltaTime);
            }

            if (_ro == null) {
                _ro = FindHighestRewindableObject(false);
            }
        }

        RewindableObject FindHighestRewindableObject(bool needsRewind) {
            int s = 0;
            RewindableObject r = null;
            foreach (RewindableObject ro in FindObjectsOfType<RewindableObject>()) {
                if (!ro.IsPositionsNull() && ro.PositionCount() > s) {
                    if (ro.IsRewinding || !needsRewind) {
                        r = ro;
                        s = ro.PositionCount();
                    }
                }
            }

            return r;
        }
    }
}
