using System;
using TMPro;
using UnityEngine;

namespace Menu {
    public class TranslateText : MonoBehaviour {
        public string translationKey;
        private void OnEnable() {
            Translate();
        }

        public void Translate() {
            GetComponent<TextMeshProUGUI>().text = Messages.Instance.GetMessage(translationKey);
        }
    }
}
