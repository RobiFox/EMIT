using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level_Specific {
    public class SceneChanger : MonoBehaviour {
        [Scene] public string scene;
        private void OnEnable() {
            SceneManager.LoadScene(scene);
        }
    }
}
