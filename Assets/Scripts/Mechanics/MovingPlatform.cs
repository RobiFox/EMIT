using System;
using Mechanics.Time_Mechanic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Mechanics {
    [System.Serializable]
    public class MovingPlatform : Toggleable {
        public Vector3 targetPlatform;
        public float speed;

        private Vector3 _origin;
        private Vector3 _target;
        
        private Material _targetMaterial;
        
        private MeshRenderer _meshRenderer;
        private RewindableObject _rewindableObject;
        private AudioSource _audioSource;

        private float _volFloat;

        public Material deactivatedMaterial;
        public Material activatedMaterial;

        public bool oneColored;

        private Vector3 _prevTransform;

        public Vector3 Target {
            get => _target;
            set => _target = value;
        }

        public Material TargetMaterial {
            get => _targetMaterial;
            set => _targetMaterial = value;
        }

        public override void Start() {
            base.Start();
            
            _meshRenderer = GetComponent<MeshRenderer>();
            _rewindableObject = GetComponent<RewindableObject>();
            _audioSource = GetComponent<AudioSource>();

            _audioSource.volume = 0f;
            
            _target = _origin = transform.localPosition;
            if (oneColored) deactivatedMaterial = activatedMaterial;
            _targetMaterial = deactivatedMaterial;
            
            _prevTransform = transform.position;
        }

        public void Update() {
            Color c = _meshRenderer.material.color;
            c = Color.Lerp(c, _targetMaterial.color, Time.deltaTime * speed);
            _meshRenderer.material.color = c;

            var position = transform.position;
            
            _audioSource.volume = Mathf.SmoothDamp(_audioSource.volume, ((_prevTransform - position).sqrMagnitude >= Math.Pow(Time.deltaTime * speed * 0.1f, 2)) ? 0.5f : 0, ref _volFloat, 0.3f);
            _prevTransform = position;
        }

        public Vector3 current;
        
        public new void FixedUpdate() {
            //transform.localPosition = Vector3.Lerp(transform.localPosition, _target, speed * Time.fixedDeltaTime);
            base.FixedUpdate();
            if(!_rewindableObject.IsRewinding) {
                Move(_target, ref current);
            }
        }

        public void Move(Vector3 target, ref Vector3 current) {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, target, ref current, 0.5f, speed, Time.fixedDeltaTime);
        }

        public override void SetStatus(bool status) {
            base.SetStatus(status);
            if (status) {
                _target = targetPlatform;
                _targetMaterial = activatedMaterial;
            } else {
                _target = _origin;
                _targetMaterial = deactivatedMaterial;
            }
        }
    }
}
