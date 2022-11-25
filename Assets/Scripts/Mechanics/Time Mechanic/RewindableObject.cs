using System.Collections.Generic;
using UnityEngine;

namespace Mechanics.Time_Mechanic {
    public abstract class RewindableObject : MonoBehaviour {
        private bool _isRewinding;

        public virtual bool IsRewinding {
            get => _isRewinding;
            set => _isRewinding = value;
        }

        private GameManager _gameManager;
        private int n;
    
        public void Start() {
            _gameManager = FindObjectOfType<GameManager>();
        }

        public void FixedUpdate() {
            if (_gameManager == null || !_gameManager.canRewind) return;
            n++;
            if (n % Mathf.RoundToInt(1 / Time.timeScale) != 0) {
                return;
            }
            n = 0;
            if (_isRewinding) {
                Rewind();
            } else {
                Record();
            }
        }

        protected abstract void Rewind();
        protected abstract void Record();
        public abstract int PositionCount();
        public abstract bool IsPositionsNull();
        
        public void OnRewindStart() { }
        public void OnRewindEnd() { }
    }
}
