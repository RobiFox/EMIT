using Audio;
using UnityEngine;

namespace Mechanics {
    public class DoorScript : Toggleable {
        private float _v;
        private MeshRenderer _mr;
        private BoxCollider _box;
        private const float Def = 0.6f;
        private float _target;

        private AudioPlayer _player;

        public override void Start() {
            _player = GetComponent<AudioPlayer>();
            
            _mr = GetComponent<MeshRenderer>();
            _box = GetComponent<BoxCollider>();
            _target = Def;
            
            base.Start();
        }
    
        void Update() {
            Color c = _mr.material.color;
            c.a = Mathf.SmoothDamp(c.a, _target, ref _v, 0.5f, Mathf.Infinity, Time.unscaledDeltaTime);
            _mr.material.color = c;
        }

        public override void SetStatus(bool invisible) {
            if (invisible != status) {
                _player.PlayAudio(_player.clips[invisible ? 0 : 1], 1f);
            }
            base.SetStatus(invisible);
            if (!invisible) {
                _box.enabled = true;
                _target = Def;
            } else {
                _box.enabled = false;
                _target = 0f;
            }
        }

        public bool CanPass() {
            return !_box.enabled;
        }
    }
}
