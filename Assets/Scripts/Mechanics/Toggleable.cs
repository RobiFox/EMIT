using System.Collections.Generic;
using Mechanics.Time_Mechanic;
using UnityEngine;

namespace Mechanics {
    public abstract class Toggleable : RewindableObject {
        public bool status;
        private List<bool> _statuses;

        public List<bool> Statuses => _statuses;

        public new virtual void Start() {
            base.Start();
            _statuses = new List<bool>();
            SetStatus(status);
        }

        public virtual void SetStatus(bool stat) {
            status = stat;
        }

        public void ToggleStatus() {
            SetStatus(!status);
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
        
            _statuses.Insert(0, status);
        }

        public override int PositionCount() {
            return _statuses.Count;
        }
        
        public override bool IsPositionsNull() {
            return _statuses == null;
        }
    }
}
