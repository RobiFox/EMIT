using UnityEngine;

namespace Level_Specific {
    public class EpilogueManager : MonoBehaviour {
        void Start() {
            AudioListener.volume = SettingsManager.instance.MasterVolume;
            PlayerPrefs.SetString(PlayerValues.CurrentProgress, "Epilogue");
        }
    }
}
