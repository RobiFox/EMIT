using Mechanics;
using UnityEditor;
using UnityEngine;

namespace Editor {
    [CustomEditor(typeof(PressureActivator))]
    public class PressureActivatorEditor : UnityEditor.Editor {
        public Mesh cubeMesh;
        public Mesh cylinderMesh;
        public override void OnInspectorGUI() {
            PressureActivator activator = (PressureActivator) target;

            base.OnInspectorGUI();

            MeshFilter mf = activator.GetComponent<MeshFilter>();

            if (!Application.isPlaying /*activator.gameObject.scene.name != null*/) {
                Mesh targetMesh = activator.playerActivable ? cylinderMesh : cubeMesh;
                if(mf.sharedMesh != targetMesh) mf.sharedMesh = activator.playerActivable ? cylinderMesh : cubeMesh;
            }
        }
    }
}
