using System.Text.RegularExpressions;
using Menu.Buttons;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu {
    public class MenuManager : MonoBehaviour {
        public Animator dialogBoxAnimator;
        public TextMeshProUGUI dialogBoxText;
        public Button yesButton;

        public GameObject backgrounds;
        
        private static readonly int Speed = Animator.StringToHash("speed");
        private static readonly int Trigger = Animator.StringToHash("trigger");

        void Start() {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            AudioListener.volume = SettingsManager.instance.MasterVolume;
            FindObjectOfType<AmbientVolume>().ambientVolume = SettingsManager.instance.AmbientVolume;
            
            string level = PlayerPrefs.GetString(PlayerValues.CurrentProgress, null);
            int background = 0;

            if (level == "Epilogue") {
                background = 4;
            } else {
                if (int.TryParse(Regex.Match(level, @"\b\d+\b").Value, out var lvl)) {
                    if (lvl >= 25) {
                        background = 3;
                    } else if (lvl >= 17) {
                        background = 2;
                        CameraShake cs = FindObjectOfType<CameraShake>();
                        cs.duration = Time.time + 10000;
                        cs.magnitude = 5;
                        cs.intensity = 1;
                    } else if (lvl >= 7) {
                        background = 1;
                        CameraShake cs = FindObjectOfType<CameraShake>();
                        cs.duration = Time.time + 10000;
                        cs.magnitude = 10;
                        cs.intensity = 1;
                    }
                }
            }

            backgrounds.transform.GetChild(background).gameObject.SetActive(true);
        }
        
        private void ShowDialog() {
            dialogBoxAnimator.SetFloat(Speed, 1f);
            dialogBoxAnimator.SetTrigger(Trigger);
            yesButton.onClick.RemoveAllListeners();
        }
        
        public void ShowExitDialog() {
            ShowDialog();
            dialogBoxText.text = Messages.Instance.GetMessage("menu.dialog.are_you_sure_to_quit");
            yesButton.onClick.AddListener(Application.Quit);
        }
        
        public void ShowStartOverwriteDialog() {
            ShowDialog();
            dialogBoxText.text = Messages.Instance.GetMessage("menu.dialog.are_you_sure_to_start_new_game");
            yesButton.onClick.AddListener(FindObjectOfType<NewGameButton>().StartGame);
        }
        
        public void HideDialog() {
            dialogBoxAnimator.SetFloat(Speed, -1f);
            dialogBoxAnimator.SetTrigger(Trigger);
        }
    }
}
