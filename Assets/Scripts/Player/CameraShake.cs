using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player {
    public class CameraShake : MonoBehaviour {
        public float duration;
        public float magnitude;
        public float intensity;

        private Quaternion _originalRotation;
    
        public void SetShake(float duration, float magnitude, float intensity = 1f) {
            this.magnitude = magnitude;
            this.duration = Time.time + duration;
            this.intensity = intensity;
        }

        void Start() {
            _originalRotation = transform.localRotation;
        }

        void Update() {
            if (duration < Time.time) {
                magnitude = Mathf.Lerp(magnitude, 0f, Time.deltaTime);
            }
            if (Math.Abs(magnitude) < 0.1f) return;
        
            Quaternion quaternion = Quaternion.Euler(Random.insideUnitSphere * Random.Range(0f, magnitude));
            transform.localRotation = Quaternion.Lerp(transform.localRotation, quaternion, Time.deltaTime * intensity);
        }
    }
}
