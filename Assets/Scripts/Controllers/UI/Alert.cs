using UnityEngine;
using UnityEngine.UI;

namespace Alchemy.Controllers {
    public class Alert : Window {
        public Text message;
        public Transform inputs;
        public Transform buttons;

        public void SetMessage(string text) {
            message.text = text;

            message.gameObject.SetActive(message.text != string.Empty);
        }

        public Toggle AddToggle(ToggleData toggleData) {
            var toggle = UserInterface.CreateToggle(toggleData);

            toggle.transform.SetParent(inputs);

            return toggle;
        }

        public Slider AddSlider(SliderData sliderData) {
            var slider = UserInterface.CreateSlider(sliderData);

            slider.transform.SetParent(inputs);

            return slider;
        }

        public Dropdown AddDropdown(DropdownData dropdownData) {
            var dropdown = UserInterface.CreateDropdown(dropdownData);

            dropdown.transform.SetParent(inputs);

            return dropdown;
        }

        public InputField AddInputField(InputFieldData inputFieldData) {
            var inputField = UserInterface.CreateInputField(inputFieldData);

            inputField.transform.SetParent(inputs);

            return inputField;
        }

        public Button AddButton(ButtonData buttonData) {
            var button = UserInterface.CreateButton(buttonData);

            button.transform.SetParent(buttons);

            return button;
        }
    }
}