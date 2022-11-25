using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu.Buttons {
    public class ContinueButton : MonoBehaviour {
        private string _level;
        private static readonly int WhiteFade = Animator.StringToHash("whiteFade");

        void Start() {
            _level = PlayerPrefs.GetString(PlayerValues.CurrentProgress, null);

            Debug.Log("Current progress is at " + _level);
            if (_level == "") {
                Destroy(gameObject);
            } else {
                Debug.Log("Can continue.");
                GetComponent<Button>().onClick.AddListener(ContinueGame);
            }
        }

        public void ContinueGame() {
            StartCoroutine(ContinueWithDelay());
        }

        private IEnumerator ContinueWithDelay() {
            GameObject.FindGameObjectWithTag("Splash").GetComponent<Animator>().SetTrigger(WhiteFade);
            yield return new WaitForSeconds(0.16f);
            SceneManager.LoadScene(_level);
        }
    }
}
