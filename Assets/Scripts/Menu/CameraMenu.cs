using System;
using UnityEngine;

namespace Menu {
    public class CameraMenu : MonoBehaviour {
        private Vector3 _originalPosition;
        private float _vel;
        private Camera _camera;

        void Start() {
            _originalPosition = transform.position;
            _camera = GetComponent<Camera>();
        }
        
        void Update() {
            var mousePos = (Input.mousePosition - new Vector3(Screen.width, Screen.height, 0) / 2f);
            mousePos.x /= Screen.width;
            mousePos.y /= Screen.height;
            mousePos = Vector3.ClampMagnitude(mousePos, 0.5f);
            transform.position = Vector3.Lerp(transform.position, _originalPosition + mousePos * 0.75f, Time.deltaTime * 2f);

            _camera.fieldOfView = Mathf.SmoothDamp(_camera.fieldOfView, 60, ref _vel, 0.3f);
        }
    }
}
