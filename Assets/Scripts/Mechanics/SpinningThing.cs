using System;
using System.Collections.Generic;
using Mechanics.Time_Mechanic;
using UnityEngine;

namespace Mechanics {
    public class SpinningThing : Toggleable {
        
        private RewindableObject _rewindableObject;
        private AudioSource _audioSource;
        private Quaternion _prevRotation;

        public float speed;

        private float _volFloat;
        
        private List<bool> _statuses;

        public Quaternion TargetRotation {
            get {
                //return Quaternion.identity;
                return Quaternion.Euler(new Vector3(0, Index * 89.9f, 0));
            }
        }

        private int Index { get; set; }

        public override void Start() {
            base.Start();
            
            _statuses = new List<bool>();
            
            _audioSource = GetComponent<AudioSource>();
            _rewindableObject = GetComponent<RewindableObject>();

            _audioSource.volume = 0f;
        }

        void Update() {
            var position = transform.position;

            var rotation = transform.rotation;
            float difference = Mathf.Abs(_prevRotation.eulerAngles.y - rotation.eulerAngles.y);
            _audioSource.volume = Mathf.SmoothDamp(_audioSource.volume, difference > 0f ? Mathf.Clamp(difference * 2, 0,1) : 0, ref _volFloat, 0.5f);
            _prevRotation = rotation;
        }

        new void FixedUpdate() {
            base.FixedUpdate();
            if (!_rewindableObject.IsRewinding) {
                transform.rotation = Quaternion.Lerp(transform.rotation, TargetRotation, speed * Time.fixedDeltaTime);
                if (Mathf.Abs(transform.rotation.eulerAngles.y - TargetRotation.eulerAngles.y) <= 1f) {
                    status = false;
                }
                
                //transform.rotation = _targetRotation;
            }
        }

        private bool _first = true;
        
        public override void SetStatus(bool s) {
            /*if (s && status != true) {
                transform.rotation = _targetRotation;
                Debug.Log("Ye, activated: " + status);
                Vector3 rotation = transform.rotation.eulerAngles;
                rotation.y += 90;
                _targetRotation = Quaternion.Euler(rotation);
            } else if (!s) {
                _targetRotation = transform.rotation;
            }*/
            if (s == status) return;
            Index += s ? 1 : -1;
            if (s) _first = false;

            status = true;
        }
        
        protected override void Rewind() {
            SetStatus(_statuses[0]);
            if (_statuses.Count > 1) {
                _statuses.RemoveAt(0);
            }
        }
        
        protected override void Record() {
            if (_statuses.Count > Mathf.Round(30f / Time.fixedDeltaTime)) {
                _statuses.RemoveAt(_statuses.Count - 1);
            }

            _statuses.Insert(0, !_first && status);
        }
        
        public override int PositionCount() {
            return _statuses.Count;
        }
    }
}
