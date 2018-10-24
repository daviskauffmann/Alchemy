using UnityEngine;
using UnityEngine.UI;

namespace Alchemy.Controllers {
    public class List : Window {
        [SerializeField]
        private Text message;
        [SerializeField]
        private Transform elements;
        [SerializeField]
        private Transform buttons;

        public void SetMessage(string text) {
            this.message.text = text;

            this.message.gameObject.SetActive(this.message.text != string.Empty);
        }

        public Toggle AddToggle(ToggleData toggleData) {
            var toggle = UserInterface.Instance.CreateToggle(toggleData);

            toggle.transform.SetParent(this.elements);

            return toggle;
        }

        public Toggle AddRadio(RadioData radioData) {
            var radio = UserInterface.Instance.CreateRadio(radioData);

            radio.transform.SetParent(this.elements);

            var group = this.elements.GetComponent<ToggleGroup>();

            if (group == null) {
                radio.group = this.elements.gameObject.AddComponent<ToggleGroup>();
            } else {
                radio.group = group;
            }

            return radio;
        }

        public Button AddButton(ButtonData buttonData) {
            var button = UserInterface.Instance.CreateButton(buttonData);

            button.transform.SetParent(this.buttons);

            return button;
        }
    }
}
