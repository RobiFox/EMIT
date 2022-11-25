using TMPro;

namespace Menu {
    public class TranslateDropdown : TranslateText {
        private void OnEnable() {
            Translate();
        }
        
        public new void Translate() {
            TMP_Dropdown dropdown = GetComponent<TMP_Dropdown>();
            int i = 0;
            foreach (TMP_Dropdown.OptionData data in dropdown.options) {
                data.text = Messages.Instance.GetMessage(translationKey + "." + i);
                i++;
            }
            dropdown.RefreshShownValue();
        }
    }
}
