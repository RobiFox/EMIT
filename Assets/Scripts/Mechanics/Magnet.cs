using System.Collections.Generic;
using Audio;
using UnityEngine;

namespace Mechanics {
    public class Magnet : Toggleable {

        public Material deactivatedMaterial;
        public Material activatedMaterial;
        
        private Material _targetMaterial;
        private MeshRenderer _meshRenderer;

        private AudioPlayer _audioPlayer;
        
        private List<Rigidbody> cubes = new List<Rigidbody>();
        
        public override void Start() {
            _meshRenderer = GetComponent<MeshRenderer>();
            _audioPlayer = GetComponent<AudioPlayer>();
            
            _targetMaterial = status ? activatedMaterial : deactivatedMaterial;

            foreach (GrabbableCube gc in FindObjectsOfType<GrabbableCube>()) {
                if(gc.GetComponent<Rigidbody>() != null)
                    cubes.Add(gc.GetComponent<Rigidbody>());
            }
            
            base.Start();
        }

        void Update() {
            Color c = _meshRenderer.material.color;
            c = Color.Lerp(c, _targetMaterial.color, Time.deltaTime);
            _meshRenderer.material.color = c;
        }

        private new void FixedUpdate() {
            base.FixedUpdate();
            if (status)
                foreach (Rigidbody rb in cubes) {
                    var position = transform.position;
                    Vector3 difference = position - rb.transform.position;
                    Physics.Raycast(position, -difference.normalized, out var hit, 10f);
                    Debug.DrawRay(position - difference.normalized, -difference.normalized, Color.cyan, Time.deltaTime);
                    if (hit.collider != null && hit.collider.gameObject == rb.gameObject && difference.sqrMagnitude < Mathf.Pow(10, 2)) {
                        rb.velocity /= 2f; // / Time.fixedDeltaTime;
                        rb.angularVelocity = Random.insideUnitSphere;
                        var mass = rb.mass;
                        rb.AddForce(
                            Vector3.ClampMagnitude((difference.normalized * mass / difference.sqrMagnitude) * 20, 3),
                            ForceMode.VelocityChange);
                    }
                }
        }

        public override void SetStatus(bool s) {
            if (status == s) return;
            base.SetStatus(s);
            _targetMaterial = s ? activatedMaterial : deactivatedMaterial;
            _audioPlayer.Pitch = Random.Range(1f, 1.1f);
            _audioPlayer.PlayAudio(_audioPlayer.clips[s ? 0 : 1], 1f);
        }
    }
}
