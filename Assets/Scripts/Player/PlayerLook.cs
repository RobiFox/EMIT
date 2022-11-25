using UnityEngine;

namespace Player {
    public class PlayerLook : MonoBehaviour {

        public Transform body;
        public bool enable = true;

        private float _xAxisClamp = 0.0f;

        void Start() {
            _xAxisClamp = (transform.rotation.eulerAngles.x - 360) % 360;
            Debug.Log("Epic " + _xAxisClamp);
        }
    
        void Awake() {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
	
        // Update is called once per frame
        void Update () {
            if (!enable) {
                return;
            }
            RotateCamera();
        }

        void RotateCamera() {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            float rotX = mouseX * SettingsManager.instance.MouseSensitivity * (SettingsManager.instance.InvertX ? -1 : 1);
            float rotY = mouseY * SettingsManager.instance.MouseSensitivity * (SettingsManager.instance.InvertY ? -1 : 1);

            _xAxisClamp -= rotY;
        
            Vector3 targetRotCam = transform.rotation.eulerAngles;
            Vector3 targetRotBody = body.rotation.eulerAngles;
        
            targetRotCam.x -= rotY;
            targetRotCam.z = 0;
            targetRotBody.y += rotX;

            _xAxisClamp = Mathf.Clamp(_xAxisClamp, -90f, 90f);
            targetRotCam.x = _xAxisClamp;

            /*if(xAxisClamp > 90) {
            xAxisClamp = 90;
            targetRotCam.x = 90;
        } else if(xAxisClamp < -90) {
            xAxisClamp = -90;
            targetRotCam.x = 270;
        }*/

            transform.rotation = Quaternion.Euler(targetRotCam);
            body.rotation = Quaternion.Euler(targetRotBody);
        }
    }
}
