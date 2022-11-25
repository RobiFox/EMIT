using Mechanics;
using UnityEditor;
using UnityEngine;

namespace Editor {
    [CustomEditor(typeof(AntiCube))]
    public class AntiCubeEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            AntiCube antiCube = (AntiCube) target;

            antiCube.distance = EditorGUILayout.Slider("Distance", antiCube.distance, 1f, 10f);
            BoxCollider collider = antiCube.GetComponent<BoxCollider>();
            Vector3 size = collider.size;
            size.z = antiCube.distance;
            collider.size = size;
            for(int i = 0; i < 2; i++) {
                Vector3 position = antiCube.transform.GetChild(i).localPosition;
                int m = (i == 0 ? 1 : -1);
                //position.z = antiCube.distance / 2f * m + m * 0.1f;
                position.z = ((-0.1f + antiCube.distance) * m)/2f;
                antiCube.transform.GetChild(i).localPosition = position;
            }

            foreach (ParticleSystem ps in antiCube.GetComponentsInChildren<ParticleSystem>()) {
                ParticleSystem.MainModule mainModule = ps.main;
                mainModule.startLifetimeMultiplier = antiCube.distance;
            }

            antiCube.status = EditorGUILayout.Toggle("Status", antiCube.status);

            if(GUI.changed)
                EditorUtility.SetDirty(antiCube);   
        }
    }
}
