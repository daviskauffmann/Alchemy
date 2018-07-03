using UnityEngine;
using UnityEngine.UI;

namespace Alchemy.Controllers {
    public class Alert : Window {
        [SerializeField]
        private Text message;
        [SerializeField]
        private Transform inputs;
        [SerializeField]
        private Transform buttons;

        public void SetMessage(string text) {
            message.text = text;

            message.gameObject.SetActive(message.text != string.Empty);
        }

        public Toggle AddToggle(ToggleData toggleData) {
            var toggle = UserInterface.Instance.CreateToggle(toggleData);

            toggle.transform.SetParent(inputs);

            return toggle;
        }

        public Slider AddSlider(SliderData sliderData) {
            var slider = UserInterface.Instance.CreateSlider(sliderData);

            slider.transform.SetParent(inputs);

            return slider;
        }

        public Dropdown AddDropdown(DropdownData dropdownData) {
            var dropdown = UserInterface.Instance.CreateDropdown(dropdownData);

            dropdown.transform.SetParent(inputs);

            return dropdown;
        }

        public InputField AddInputField(InputFieldData inputFieldData) {
            var inputField = UserInterface.Instance.CreateInputField(inputFieldData);

            inputField.transform.SetParent(inputs);

            return inputField;
        }

        public Button AddButton(ButtonData buttonData) {
            var button = UserInterface.Instance.CreateButton(buttonData);

            button.transform.SetParent(buttons);

            return button;
        }
    }
}