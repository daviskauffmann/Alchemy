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
            message.text = text;

            message.gameObject.SetActive(message.text != string.Empty);
        }

        public Toggle AddToggle(ToggleData toggleData) {
            var toggle = UserInterface.Instance.CreateToggle(toggleData);

            toggle.transform.SetParent(elements);

            return toggle;
        }

        public Toggle AddRadio(RadioData radioData) {
            var radio = UserInterface.Instance.CreateRadio(radioData);

            radio.transform.SetParent(elements);

            var group = elements.GetComponent<ToggleGroup>();

            if (group == null) {
                radio.group = elements.gameObject.AddComponent<ToggleGroup>();
            } else {
                radio.group = group;
            }

            return radio;
        }

        public Button AddButton(ButtonData buttonData) {
            var button = UserInterface.Instance.CreateButton(buttonData);

            button.transform.SetParent(buttons);

            return button;
        }
    }
}