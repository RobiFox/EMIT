using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Mechanics {
    public class AndGate : MonoBehaviour {
        public bool[] values;
        [Serializable] public class OnActivate : UnityEvent { }
        [SerializeField] private OnActivate onActivate = new OnActivate();
        [SerializeField] private OnActivate onDeactivate = new OnActivate();

        void Start() {
            Destroy(GetComponent<MeshRenderer>());
        }

        public void EnableIndex(int index) {
            SetIndex(index, true);
        }
        
        public void DisableIndex(int index) {
            SetIndex(index, false);
        }

        public void SetIndex(int index, bool value) {
            values[index] = value;
            if (Evaluate()) {
                onActivate.Invoke();
            } else {
                onDeactivate.Invoke();
            }
        }

        private bool Evaluate() {
            return values.All(b => b);
        }
    }
}
