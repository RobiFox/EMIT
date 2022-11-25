using System.Collections;
using Cutscene;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu.Buttons {
    public class NewGameButton : MonoBehaviour {
        private static readonly int BlackFade = Animator.StringToHash("blackFade");
        
        public void PressButton() {
            string level = PlayerPrefs.GetString(PlayerValues.CurrentProgress, null);
            if (level == "") {
                StartGame();
            } else {
                FindObjectOfType<MenuManager>().ShowStartOverwriteDialog();
            }
        }

        public void StartGame() {
            StartCoroutine(ContinueWithDelay());
        }
        
        private IEnumerator ContinueWithDelay() {
            GameObject.FindGameObjectWithTag("Splash").GetComponent<Animator>().SetTrigger(BlackFade);
            yield return new WaitForSeconds(0.2f);
            SceneManager.LoadScene("Beginning");
        }
    }
}