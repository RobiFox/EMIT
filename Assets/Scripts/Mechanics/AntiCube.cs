using Audio;
using Player;
using UnityEngine;

namespace Mechanics {
    [SelectionBase]
    public class AntiCube : Toggleable {
        public float distance;

        public ParticleSystem[] particles;

        public Material activatedMaterial;
        public Material deactivatedMaterial;

        private Material _targetMaterial;
        private MeshRenderer[] _meshRenderer;

        private float _initialSimulationSpeed = 0f;
        private float _targetSimulationSpeed = 0f;
        private float _simulationSpeed = 0f;

        private float _vel;

        private AudioPlayer _player;

        public override void Start() {
            _meshRenderer = GetComponentsInChildren<MeshRenderer>();
            _targetMaterial = status ? activatedMaterial : deactivatedMaterial;

            _targetSimulationSpeed = _initialSimulationSpeed = particles[0].main.simulationSpeed;
            _simulationSpeed = _targetSimulationSpeed * 10f;

            _player = GetComponent<AudioPlayer>();
            
            foreach (CharacterController cc in FindObjectsOfType<CharacterController>()) {
                Physics.IgnoreCollision(cc, GetComponent<Collider>(), true);
                Physics.IgnoreCollision(cc, transform.GetChild(0).GetComponent<Collider>(), true);
                Physics.IgnoreCollision(cc, transform.GetChild(1).GetComponent<Collider>(), true);
            }

            base.Start();
        }

        void Update() {
            foreach (MeshRenderer mr in _meshRenderer) {
                Color c = mr.material.color;
                c = Color.Lerp(c, _targetMaterial.color, Time.deltaTime);
                mr.material.color = c;
            }

            _simulationSpeed = Mathf.SmoothDamp(_simulationSpeed, _targetSimulationSpeed, ref _vel, 1f);

            foreach (ParticleSystem ps in particles) {
                ParticleSystem.MainModule mainModule = ps.main;
                mainModule.simulationSpeed = _simulationSpeed;
            }
        }
        

        public override void SetStatus(bool stat) {
            if (stat != status) {
                _player.PlayAudio(_player.clips[stat ? 0 : 1], 1f);
            }
            base.SetStatus(stat);
            _targetMaterial = stat ? activatedMaterial : deactivatedMaterial;
            FindObjectOfType<ObjectGrabber>().RefreshAntiCube(this);
            gameObject.layer = stat ? 0 : 2;
            foreach (ParticleSystem ps in particles) {
                if (stat) {
                    ps.Play();
                    _targetSimulationSpeed = _initialSimulationSpeed;
                } else {
                    ps.Stop();
                    _targetSimulationSpeed = _initialSimulationSpeed * 10;
                }
            }
            foreach(GrabbableCube cube in FindObjectsOfType<GrabbableCube>()){
                Physics.IgnoreCollision(cube.GetComponent<Collider>(), GetComponent<Collider>(), !stat);
            }
        }
    }
}
