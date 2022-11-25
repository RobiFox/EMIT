using System.Collections;
using Cutscene;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu.Buttons {
    public class OptionsButton : MonoBehaviour {
        public GameObject _settingsManager;

        public void ShowOptions() {
            _settingsManager.SetActive(true);
        }
    }
}