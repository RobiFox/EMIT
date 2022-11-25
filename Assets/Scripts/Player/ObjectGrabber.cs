using System.Collections.Generic;
using Mechanics;
using Mechanics.Time_Mechanic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player {
    public class ObjectGrabber : MonoBehaviour {
        public GameObject theCube;

        private bool _isHolding;

        public bool IsHolding {
            get { return _isHolding; }
            private set {
                _isHolding = value;
                if (value && SettingsManager.instance.ShowCube == 0) {
                    _cubeHelper.SetActive(false);
                } else {
                    _cubeHelper.SetActive(value);
                    _cubeHelper.GetComponent<MeshFilter>().mesh = grabbed.GetComponent<MeshFilter>().mesh;
                }

                foreach (AntiCube ac in FindObjectsOfType<AntiCube>()) {
                    RefreshAntiCube(ac);
                }
            }
        }

        public void RefreshAntiCube(AntiCube antiCube) {
            Physics.IgnoreCollision(antiCube.GetComponent<Collider>(), GetComponentInParent<CharacterController>(), !antiCube.status || !_isHolding);
        }

        [FormerlySerializedAs("_grabbed")] public GrabbableCube grabbed;
        //private Vector3 moveTowards;
        private GameManager _gm;
        private const float DefaultDistance = 2f;
        private Animator _animator;
        private static readonly int Cube = Animator.StringToHash("cube");
        private static readonly int Rip = Animator.StringToHash("rip");
        private const int Timer = 18;

        private GameObject _cubeHelper;

        public GrabbableCube Grabbed => grabbed;

        public void Start() {
            _gm = FindObjectOfType<GameManager>();
            _isHolding = false;
            theCube.SetActive(false);
            _animator = transform.root.GetComponent<Animator>();
            _cubeHelper = GameObject.FindGameObjectWithTag("CubeHelper");
            _cubeHelper.SetActive(false);
        }

        private void FixedUpdate() {
        }

        public void Update() {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.blue);
            if (_isHolding && SettingsManager.instance.ShowCube > 0) {
                var (position, rotation) = GetTransformForCube(_cubeHelper);
                if ((position - _cubeHelper.transform.position).sqrMagnitude < 4) {
                    _cubeHelper.transform.position = Vector3.Lerp(_cubeHelper.transform.position, position, Time.deltaTime * 10);
                    _cubeHelper.transform.rotation = Quaternion.Lerp(_cubeHelper.transform.rotation, rotation, Time.deltaTime * 10);
                } else {
                    _cubeHelper.transform.position = position;
                    _cubeHelper.transform.rotation = rotation;
                }
            }
            if (Input.GetKeyDown(KeyCode.E)) {
                if (_isHolding) {
                    DropCube();
                } else {
                    var hit = Raycast();
                    if (hit.collider != null) {
                        GrabCube(hit);
                    }
                }
            }
        }

        public virtual void GrabCube(RaycastHit hit) {
            if (hit.transform.GetComponent<GrabbableCube>() != null && hit.transform.GetComponent<GrabbableCube>().IsAvailableForGrab()) {
                _gm.StopAction = true;
                print("yeet");
                (grabbed = hit.transform.GetComponent<GrabbableCube>()).IsGrabbed = true;
                IsHolding = true;
                var transform1 = grabbed.transform;
                var position = transform1.position;
                transform1.position = position;
                _cubeHelper.transform.position = position;
                _cubeHelper.transform.rotation = transform1.rotation;
                RewindableObject ro = grabbed.GetComponent<RewindableObject>();
                theCube.SetActive(true);
                if(SettingsManager.instance.ShowCube < 2) _animator.SetTrigger(Cube);

                /*if (ro == null) return;
            foreach (var vector in GetInbetween(_grabbed.transform.position, transform.position)) {
                ro.AddPosition(new TimeFrame(vector, _grabbed.transform.rotation));
            }*/
            } else if (hit.transform.GetComponent<ButtonActivator>() != null) {
                hit.transform.GetComponent<ButtonActivator>().SetStatus(!hit.transform.GetComponent<ButtonActivator>().status);
            }
        }

        public RaycastHit Raycast() {
            Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit, 2);
            return hit;
        }
    
        public (Vector3, Quaternion) GetTransformForCube(GameObject go) {
            var hit = Raycast();
            Vector3 position;
            Quaternion rotation;
            if (hit.collider != null) {
                position = hit.point + hit.normal * go.transform.localScale.x;
                rotation = hit.collider.transform.rotation;
            } else {
                position = transform.position + transform.TransformDirection(Vector3.forward) * (2 * go.transform.localScale.x);
                rotation = Quaternion.identity;
            }
            return (position, rotation);
        }

        public void ApplyTransformForCube(GameObject go) {
            var (vector3, quaternion) = GetTransformForCube(go);
            Debug.Log("Dropping on position " + vector3);
            go.transform.position = vector3;
            go.transform.rotation = quaternion;
        }

        public virtual void DropCube() {
            //Debug.DrawRay(hit.point, hit.normal, Color.green, 1000);
            ApplyTransformForCube(grabbed.gameObject);
            grabbed.IsGrabbed = false;
            IsHolding = false;
            if(SettingsManager.instance.ShowCube < 2) _animator.SetTrigger(Rip);
                
            /*
         if(ro != null)
            foreach (var vector in GetInbetween(_grabbed.transform.position, transform.position).Reverse()) {
                ro.AddPosition(new TimeFrame(vector, _grabbed.transform.rotation));
            }*/
            
            grabbed.GetComponent<Rigidbody>().velocity = Vector3.zero;
            grabbed.GetComponent<Rigidbody>().AddForce(FindObjectOfType<PlayerMove>().MoveDirection, ForceMode.VelocityChange);
            grabbed = null;
        }

        private IEnumerable<Vector3> GetInbetween(Vector3 a, Vector3 b) {
            Vector3 v = b - a;
            Vector3[] vectors = new Vector3[Timer];

            for (int i = 0; i < Timer; i++) {
                vectors[i] = (v * (i + 1)) / Timer + a;
            }

            return vectors;
        }
    }
}
