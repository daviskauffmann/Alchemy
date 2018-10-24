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
            this.message.text = text;

            this.message.gameObject.SetActive(this.message.text != string.Empty);
        }

        public Toggle AddToggle(ToggleData toggleData) {
            var toggle = UserInterface.Instance.CreateToggle(toggleData);

            toggle.transform.SetParent(this.inputs);

            return toggle;
        }

        public Slider AddSlider(SliderData sliderData) {
            var slider = UserInterface.Instance.CreateSlider(sliderData);

            slider.transform.SetParent(this.inputs);

            return slider;
        }

        public Dropdown AddDropdown(DropdownData dropdownData) {
            var dropdown = UserInterface.Instance.CreateDropdown(dropdownData);

            dropdown.transform.SetParent(this.inputs);

            return dropdown;
        }

        public InputField AddInputField(InputFieldData inputFieldData) {
            var inputField = UserInterface.Instance.CreateInputField(inputFieldData);

            inputField.transform.SetParent(this.inputs);

            return inputField;
        }

        public Button AddButton(ButtonData buttonData) {
            var button = UserInterface.Instance.CreateButton(buttonData);

            button.transform.SetParent(this.buttons);

            return button;
        }
    }
}
