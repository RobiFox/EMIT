using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Mechanics {
    public class PressureActivator : MonoBehaviour {
        [Serializable] public class OnActivate : UnityEvent { }
        [SerializeField] private OnActivate onActivate = new OnActivate();
        [SerializeField] private OnActivate onDeactivate = new OnActivate();

        public bool playerActivable;

        private bool _active;
        private int _amount = 0;
        private float _startTime;

        private const float Modifier = 20f;

        public int amount {
            get => _amount;
            set {
                _amount = value;
                active = _amount > 0;
            }
        }

        public bool active {
            get { return _active; }
            set {
                if (active == value) {
                    _active = value;
                    return;
                }
                _active = value;
                if (value) {
                    Debug.Log("ACTIVE!!");
                    onActivate.Invoke();
                } else {
                    Debug.Log("deactive");
                    onDeactivate.Invoke();
                }
            }
        }
        
        
        private void Awake() {
            _startTime = Time.time;
        }

        private void OnTriggerEnter(Collider other) {
            GrabbableCube gc = other.GetComponent<GrabbableCube>();
            if (gc != null) {
                Rigidbody rigidBody = gc.GetComponent<Rigidbody>();
                amount++;
                gc.particle = true;
                rigidBody.velocity /= Modifier;
                rigidBody.useGravity = false;
                if(Time.time - _startTime > 1f) 
                    if (gc.transform.position.y - transform.position.y > 2)
                        rigidBody.AddForce(Vector3.up * -0.0125f, ForceMode.Impulse);
                    else rigidBody.AddForce(Vector3.up * 0.0125f, ForceMode.Impulse);
                rigidBody.angularVelocity = Random.onUnitSphere * 0.2f;
                gc.pressureActivator = this;
            } else if(other.CompareTag("Player") && playerActivable) {
                amount++;
            }
        }
    
        private void OnTriggerExit(Collider other) {
            GrabbableCube gc = other.GetComponent<GrabbableCube>();
            if (gc != null) {
                gc.particle = false;
                gc.GetComponent<Rigidbody>().useGravity = true;
                amount--;
                gc.pressureActivator = null;
                gc.GetComponent<Rigidbody>().velocity *= Modifier * 0.25f;
            } else if(other.GetComponent<PlayerMove>() != null && playerActivable) {
                amount--;
            }
        }

        public void TriggerTriggerExit(Collider other) {
            OnTriggerExit(other);
        }

        public void VerifyStatus() {
            Debug.Log("VERIFYING " + amount + "_" + active);
            if (amount > 0) {
                _active = true;
            } else if (amount == 0) {
                _active = false;
            }
            InvokeEvent();
        }

        public void InvokeEvent() {
            if (active) onActivate.Invoke();
            else onDeactivate.Invoke();
        }
    }
}
