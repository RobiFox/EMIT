using UnityEngine;
using UnityEngine.UI;

namespace Menu.Settings {
    public class LanguageButton : MonoBehaviour {
        public SystemLanguage language;
        void Start() {
            GetComponent<Button>().onClick.AddListener(SetLanguage);
        }

        private void SetLanguage() {
            Messages.Instance.LoadLanguage(language);
            PlayerPrefs.SetString(PlayerValues.Language, language.ToString());
            foreach (TranslateText tt in FindObjectsOfType<TranslateText>()) {
                tt.Translate();
            }
        }
    }
}
