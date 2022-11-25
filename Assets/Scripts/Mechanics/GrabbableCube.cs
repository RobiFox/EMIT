using System;
using Audio;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Mechanics {
    public class GrabbableCube : MonoBehaviour {
        private bool _isGrabbed;
        private float _v;
        private MeshRenderer _mr;
        private float _target;
        private BoxCollider _box;
        private Rigidbody _rigid;
        [HideInInspector] public float def = 1;

        private AudioPlayer _audioPlayer;

        private GameObject _particle;

        private Vector3 _prevPosition;

        public float Target {
            get => _target;
            set => _target = value;
        }

        public bool particle {
            get { return _particle.gameObject.activeSelf; }
            set {
                _particle.gameObject.SetActive(value);
            }
        }

        public PressureActivator pressureActivator;

        public bool IsGrabbed {
            get => _isGrabbed;
            set {
                _isGrabbed = value;
                _box.enabled = !value;
                _rigid.isKinematic = value;
            
                _target = value ? 0 : def;
                _box.isTrigger = value;

                if (!value) return;
                if (pressureActivator != null) {
                    pressureActivator.TriggerTriggerExit(_box);
                }
            }
        }

        public void Update() {
            Color c = _mr.material.color;
            c.a = Mathf.SmoothDamp(c.a, _target, ref _v, 0.05f);
            _mr.material.color = c;
            
            _prevPosition = transform.position;

            /*if (Math.Abs(c.a - 1f) < 0.1 && _mr.material.) {
            
        }*/
        }

        private void OnCollisionEnter(Collision other) {
            float volume = Mathf.Clamp((transform.position - _prevPosition).magnitude * 10, 0, 1);
            _audioPlayer.Pitch /= Mathf.Abs(_audioPlayer.Pitch);
            _audioPlayer.Pitch *= Random.Range(0.5f, 1.5f);
            _audioPlayer.PlayAudio(volume);
            if (other.gameObject.GetComponent<GrabbableCube>() != null) {
                AudioPlayer ap = other.gameObject.GetComponent<AudioPlayer>();
                ap.Pitch /= Mathf.Abs(ap.Pitch);
                ap.Pitch *= Random.Range(0.5f, 1.5f);
                ap.PlayAudio(volume);
            }
        }

        public void Start() {
            _particle = transform.GetChild(0).gameObject;
            _particle.SetActive(false);
            _isGrabbed = false;
            _mr = GetComponent<MeshRenderer>();
            _target = def;
            _box = GetComponent<BoxCollider>();
            _rigid = GetComponent<Rigidbody>();
            _audioPlayer = GetComponent<AudioPlayer>();
        }

        public virtual bool IsAvailableForGrab() {
            return true;
        }
    }
}
