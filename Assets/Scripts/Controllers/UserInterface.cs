using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Alchemy.Controllers {
    public class UserInterface : MonoBehaviour {
        [SerializeField]
        private Canvas canvas;
        [SerializeField]
        private Text textPrefab;
        [SerializeField]
        private Button buttonPrefab;
        [SerializeField]
        private Toggle togglePrefab;
        [SerializeField]
        private Toggle radioPrefab;
        [SerializeField]
        private Slider sliderPrefab;
        [SerializeField]
        private Scrollbar scrollbarPrefab;
        [SerializeField]
        private Dropdown dropdownPrefab;
        [SerializeField]
        private InputField inputFieldPrefab;
        [SerializeField]
        private ScrollRect scrollViewPrefab;
        [SerializeField]
        private Window windowPrefab;
        [SerializeField]
        private Alert alertPrefab;
        [SerializeField]
        private List listPrefab;

        public static UserInterface Instance { get; private set; }

        private void Awake() => Instance = this;

        public Button CreateButton(ButtonData buttonData) {
            var button = Instantiate(this.buttonPrefab, this.canvas.transform, false);

            button.GetComponentInChildren<Text>().text = buttonData.text;

            if (buttonData.onClick != null) {
                button.onClick.AddListener(buttonData.onClick);
            }

            return button;
        }

        public Toggle CreateToggle(ToggleData toggleData) {
            var toggle = Instantiate(this.togglePrefab, this.canvas.transform, false);

            toggle.GetComponentInChildren<Text>().text = toggleData.text;

            toggle.isOn = toggleData.isOn;

            if (toggleData.onValueChanged != null) {
                toggle.onValueChanged.AddListener(toggleData.onValueChanged);
            }

            return toggle;
        }

        public Toggle CreateRadio(RadioData radioData) {
            var radio = Instantiate(this.radioPrefab, this.canvas.transform, false);

            radio.GetComponentInChildren<Text>().text = radioData.text;

            radio.isOn = radioData.isOn;

            if (radioData.onValueChanged != null) {
                radio.onValueChanged.AddListener(radioData.onValueChanged);
            }

            return radio;
        }

        public Slider CreateSlider(SliderData sliderData) {
            var slider = Instantiate(this.sliderPrefab, this.canvas.transform, false);

            return slider;
        }

        public Dropdown CreateDropdown(DropdownData dropdownData) {
            var dropdown = Instantiate(this.dropdownPrefab, this.canvas.transform, false);

            if (dropdownData.options != null) {
                dropdown.options = dropdownData.options;
            }

            if (dropdownData.onValueChanged != null) {
                dropdown.onValueChanged.AddListener(dropdownData.onValueChanged);
            }

            return dropdown;
        }

        public InputField CreateInputField(InputFieldData inputFieldData) {
            var inputField = Instantiate(this.inputFieldPrefab, this.canvas.transform, false);

            if (inputFieldData.onValueChanged != null) {
                inputField.onValueChanged.AddListener(inputFieldData.onValueChanged);
            }

            if (inputFieldData.onEndEdit != null) {
                inputField.onEndEdit.AddListener(inputFieldData.onEndEdit);
            }

            return inputField;
        }

        public Window CreateWindow(WindowData windowData) {
            var window = Instantiate(this.windowPrefab, this.canvas.transform, false);

            window.SetTitle(windowData.title);

            if (windowData.onUpdate != null) {
                window.AddUpdateListener(windowData.onUpdate);
            }

            if (windowData.onClose != null) {
                window.AddCloseListener(windowData.onClose);
            }

            return window;
        }

        public Alert CreateAlert(AlertData alertData) {
            var alert = Instantiate(this.alertPrefab, this.canvas.transform, false);

            alert.SetTitle(alertData.title);

            if (alertData.onUpdate != null) {
                alert.AddUpdateListener(alertData.onUpdate);
            }

            if (alertData.onClose != null) {
                alert.AddCloseListener(alertData.onClose);
            }

            alert.SetMessage(alertData.message);

            if (alertData.inputs != null) {
                foreach (var inputData in alertData.inputs) {
                    if (inputData is ToggleData) {
                        var toggleData = (ToggleData)inputData;

                        alert.AddToggle(toggleData);
                    } else if (inputData is SliderData) {
                        var sliderData = (SliderData)inputData;

                        alert.AddSlider(sliderData);
                    } else if (inputData is DropdownData) {
                        var dropdownData = (DropdownData)inputData;

                        alert.AddDropdown(dropdownData);
                    } else if (inputData is InputFieldData) {
                        var inputFieldData = (InputFieldData)inputData;

                        alert.AddInputField(inputFieldData);
                    }
                }
            }

            if (alertData.buttons != null) {
                foreach (var buttonData in alertData.buttons) {
                    alert.AddButton(buttonData);
                }
            }

            return alert;
        }

        public List CreateList(ListData listData) {
            var list = Instantiate(this.listPrefab, this.canvas.transform, false);

            list.SetTitle(listData.title);

            if (listData.onUpdate != null) {
                list.AddUpdateListener(listData.onUpdate);
            }

            if (listData.onClose != null) {
                list.AddCloseListener(listData.onClose);
            }

            list.SetMessage(listData.message);

            if (listData.elements != null) {
                foreach (var element in listData.elements) {
                    if (element is ToggleData) {
                        var toggleData = (ToggleData)element;

                        list.AddToggle(toggleData);
                    } else if (element is RadioData) {
                        var radioData = (RadioData)element;

                        list.AddRadio(radioData);
                    }
                }
            }

            if (listData.buttons != null) {
                foreach (var buttonData in listData.buttons) {
                    list.AddButton(buttonData);
                }
            }

            return list;
        }
    }

    public interface IWindowData {
        string title { get; set; }
        UnityAction<Window> onUpdate { get; set; }
        UnityAction<Window> onClose { get; set; }
    }

    public interface IInputData {

    }

    public interface IListData {

    }

    public struct TextData : IListData {
        public string text { get; set; }
    }

    public struct ButtonData {
        public string text { get; set; }
        public UnityAction onClick { get; set; }
    }

    public struct ToggleData : IInputData, IListData {
        public string text { get; set; }
        public bool isOn { get; set; }
        public UnityAction<bool> onValueChanged { get; set; }
    }

    public struct RadioData : IInputData, IListData {
        public string text { get; set; }
        public bool isOn { get; set; }
        public UnityAction<bool> onValueChanged { get; set; }
    }

    public struct SliderData : IInputData {

    }

    public struct DropdownData : IInputData {
        public List<Dropdown.OptionData> options { get; set; }
        public UnityAction<int> onValueChanged { get; set; }
    }

    public struct InputFieldData : IInputData {
        public UnityAction<string> onValueChanged { get; set; }
        public UnityAction<string> onEndEdit { get; set; }
    }

    public struct WindowData : IWindowData {
        public string title { get; set; }
        public UnityAction<Window> onUpdate { get; set; }
        public UnityAction<Window> onClose { get; set; }
    }

    public struct AlertData : IWindowData {
        public string title { get; set; }
        public UnityAction<Window> onUpdate { get; set; }
        public UnityAction<Window> onClose { get; set; }
        public string message { get; set; }
        public IInputData[] inputs { get; set; }
        public ButtonData[] buttons { get; set; }
    }

    public struct ListData : IWindowData {
        public string title { get; set; }
        public UnityAction<Window> onUpdate { get; set; }
        public UnityAction<Window> onClose { get; set; }
        public string message { get; set; }
        public IListData[] elements { get; set; }
        public ButtonData[] buttons { get; set; }
    }
}
