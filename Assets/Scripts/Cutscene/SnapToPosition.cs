using System;
using Player;
using UnityEngine;
using UnityEngine.Playables;

namespace Cutscene {
    public class SnapToPosition : MonoBehaviour {
        public float speed;
        public PlayableDirector director;
        private Vector3 _targetPos;
        private Quaternion _targetRot;

        private bool _snap;

        private void Start() {
            director.gameObject.SetActive(false);
            _targetPos = director.transform.position;
            _targetRot = director.transform.rotation;
        }

        private void Update() {
            if (!_snap) return;
            Camera cam = Camera.main;
            if (cam == null) return;
            cam.transform.position = Vector3.Lerp(cam.transform.position, _targetPos, speed * Time.deltaTime);
            cam.transform.rotation =
                Quaternion.Lerp(cam.transform.rotation, _targetRot, speed * Time.deltaTime);

            if ((cam.transform.position - _targetPos).sqrMagnitude < 0.1f && (cam.transform.rotation.eulerAngles - _targetRot.eulerAngles).sqrMagnitude < 0.1f) {
                _snap = false;
                director.Play();
            }
        }

        public void StartCutscene() {
            director.gameObject.SetActive(true);
            FindObjectOfType<PlayerLook>().enable = false;
            FindObjectOfType<PlayerMove>().enable = false;
            _snap = true;
        }
    }
}
