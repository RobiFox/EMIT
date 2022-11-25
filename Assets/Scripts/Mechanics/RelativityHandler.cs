using UnityEngine;

namespace Mechanics {
    public class RelativityHandler : MonoBehaviour {
        void Update() {
            Debug.DrawRay(transform.position,-Vector3.up, Color.magenta);
            
            if (Physics.Raycast(new Ray(transform.position,-Vector3.up), out var hit, 1f)) {
                if (hit.collider.CompareTag("Relativity")) {
                    Transform hitTransform = hit.transform;
                    var child = hitTransform.childCount > 0 ? hitTransform.GetChild(0).transform : hitTransform;
                    transform.SetParent(child);
                } else {
                    transform.SetParent(null);
                }
            }
        }
    }
}