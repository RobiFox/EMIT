using UnityEngine;

namespace Menu.Settings {
    public class ElementChooser : MonoBehaviour {
        public GameObject contents;
        
        void Start() {
            ShowContent(0);
        }

        private void HideContent() {
            for (int i = 0; i < contents.transform.childCount; i++) {
                contents.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        public void ShowContent(int id) {
            HideContent();
            contents.transform.GetChild(id).gameObject.SetActive(true);
        }
    }
}
