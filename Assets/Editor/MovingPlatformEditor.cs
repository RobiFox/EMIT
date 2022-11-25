using Mechanics;
using UnityEditor;
using UnityEngine;

namespace Editor {
    [CustomEditor(typeof(MovingPlatform))]
    public class MovingPlatformEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            MovingPlatform platform = (MovingPlatform) target;
            
            platform.targetPlatform = EditorGUILayout.Vector3Field("Target Position", platform.targetPlatform);
            
            if (GUILayout.Button("Set Target to Current Position")) {
                platform.targetPlatform = Selection.activeTransform.localPosition;
            }

            platform.speed = EditorGUILayout.Slider("Speed", platform.speed, 0.1f, 10f);
            platform.oneColored = EditorGUILayout.Toggle("One Colored", platform.oneColored);
            if (!Application.isPlaying && platform.gameObject.scene.name != null) {
                Material targetMaterial =
                    platform.oneColored ? platform.activatedMaterial : platform.deactivatedMaterial;
                MeshRenderer mr = platform.GetComponent<MeshRenderer>();
                if(mr.sharedMaterial != targetMaterial) {
                    mr.sharedMaterial = targetMaterial;
                }
            }

            Transform transform;
            Vector3 scale = (transform = platform.transform).GetChild(0).localScale;
            var localScale = transform.localScale;
            scale.x = 1/localScale.x;
            scale.y = 1/localScale.y;
            scale.z = 1/localScale.z;

            transform.GetChild(0).localScale = scale;

            platform.status = EditorGUILayout.Toggle("Status", platform.status);
            
            /*platform.activatedMaterial = (Material) EditorGUILayout.ObjectField("Activated Material", platform.activatedMaterial, typeof(Material), true);
            platform.deactivatedMaterial = (Material) EditorGUILayout.ObjectField("Deactivated Material", platform.deactivatedMaterial, typeof(Material), true);*/
            
            if(GUI.changed)
                EditorUtility.SetDirty(platform);   
        }
    }
}
