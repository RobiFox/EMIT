using System.Collections.Generic;
using UnityEngine;

namespace Mechanics.Time_Mechanic {
    public class RewindablePosition : RewindableObject {
        private List<TimeFrame> _positions;

        private Rigidbody _rigidBody;
        private RigidbodyConstraints _constraints;
        private GrabbableCube _grabbable;

        public override bool IsRewinding {
            get { return base.IsRewinding; }
            set {
                base.IsRewinding = value;
                if (_rigidBody == null) return;
                if (value) {
                    _rigidBody.constraints = RigidbodyConstraints.FreezeAll;
                } else { 
                    RestoreConstraints();
                }
            }
        }

        public new void Start() {
            base.Start();
            _positions = new List<TimeFrame>();
            _rigidBody = GetComponent<Rigidbody>();
            if(_rigidBody != null) _constraints = _rigidBody.constraints;
            _grabbable = GetComponent<GrabbableCube>();
        }

        protected override void Rewind() {
            transform.position = (transform.position - _positions[0].position).sqrMagnitude <= 16 ? Vector3.MoveTowards(transform.position, _positions[0].position, UnityEngine.Time.deltaTime * 10) : _positions[0].position;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _positions[0].rotation, UnityEngine.Time.deltaTime * 360);
            if (_positions.Count > 1) {
                _positions.RemoveAt(0);
            }
        }

        protected override void Record() {
            var t = transform;
            bool b = _grabbable != null && _grabbable.IsGrabbed;
            AddPosition(new TimeFrame(b ? GameManager.Player.transform.position : t.position,
                b ? GameManager.Player.transform.rotation : t.rotation));
        }

        public void AddPosition(TimeFrame tf) {
            if (_positions.Count > Mathf.Round(30f / UnityEngine.Time.fixedDeltaTime)) {
                _positions.RemoveAt(_positions.Count - 1);
            }
        
            _positions.Insert(0, tf);
        }

        public void RestoreConstraints() {
            if(_rigidBody != null) _rigidBody.constraints = _constraints;
        }

        public override int PositionCount() {
            return _positions.Count;
        }

        public override bool IsPositionsNull() {
            return _positions == null;
        }
    }
}
