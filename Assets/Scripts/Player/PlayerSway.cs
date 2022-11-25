using UnityEngine;

namespace Player {
    public class PlayerSway : MonoBehaviour {
        public GameObject itemHolder;
        private PlayerLook look;
        private Vector3 origin;
        [SerializeField] private float intensity;
        [SerializeField] private float smooth;
        [SerializeField] private float maxRange;
        void Start() {
            look = FindObjectOfType<PlayerLook>();
            origin = itemHolder.transform.localPosition;
        }

        private Vector3 vel;
        
        void Update() {
            float rotX = Mathf.Clamp(Input.GetAxis("Mouse X") * SettingsManager.instance.MouseSensitivity, -maxRange, maxRange) * intensity;
            float rotY = Mathf.Clamp(Input.GetAxis("Mouse Y") * SettingsManager.instance.MouseSensitivity, -maxRange, maxRange) * intensity;

            Vector3 finalPos = new Vector3(-rotX, -rotY, 0);

            itemHolder.transform.localPosition = Vector3.Lerp(itemHolder.transform.localPosition, finalPos + origin, Time.deltaTime * smooth);
        }
    }
}
