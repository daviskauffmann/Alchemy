using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Alchemy.Controllers
{
	public class UserInterface : MonoBehaviour
	{
		public static UserInterface instance;

		[SerializeField]
		Canvas canvas;
		[SerializeField]
		Text textPrefab;
		[SerializeField]
		Button buttonPrefab;
		[SerializeField]
		Toggle togglePrefab;
		[SerializeField]
		Toggle radioPrefab;
		[SerializeField]
		Slider sliderPrefab;
		[SerializeField]
		Scrollbar scrollbarPrefab;
		[SerializeField]
		Dropdown dropdownPrefab;
		[SerializeField]
		InputField inputFieldPrefab;
		[SerializeField]
		ScrollRect scrollViewPrefab;
		[SerializeField]
		Window windowPrefab;
		[SerializeField]
		Alert alertPrefab;
		[SerializeField]
		ToggleList toggleListPrefab;
		[SerializeField]
		RadioList radioListPrefab;

		[SerializeField]
		Text messages;

		void Awake()
		{
			instance = this;
		}

		void Start()
		{
			// TEST
			GameManager.instance.world.ApplicantReceived += (sender, e) =>
			{
				var color = "";
				switch (e.employee.Title)
				{
					case "Apothecary":
						color = "aqua";
						break;
					case "Guard":
						color = "red";
						break;
					case "Herbalist":
						color = "green";
						break;
					case "Shopkeeper":
						color = "yellow";
						break;
				}
				messages.text += e.employee.Name + " the <color=" + color + ">" + e.employee.Title + "</color> has applied for a job." + "\n";
			};
		}

		public static Button CreateButton(ButtonData buttonData)
		{
			var button = Instantiate(instance.buttonPrefab, instance.canvas.transform, false);
			button.GetComponentInChildren<Text>().text = buttonData.text;
			if (buttonData.onClick != null)
			{
				button.onClick.AddListener(buttonData.onClick);
			}
			return button;
		}

		public static Toggle CreateToggle(ToggleData toggleData)
		{
			var toggle = Instantiate(instance.togglePrefab, instance.canvas.transform, false);
			toggle.GetComponentInChildren<Text>().text = toggleData.text;
			toggle.isOn = toggleData.isOn;
			if (toggleData.onValueChanged != null)
			{
				toggle.onValueChanged.AddListener(toggleData.onValueChanged);
			}
			return toggle;
		}

		public static Toggle CreateRadio(ToggleData toggleData)
		{
			var radio = Instantiate(instance.radioPrefab, instance.canvas.transform, false);
			radio.GetComponentInChildren<Text>().text = toggleData.text;
			radio.isOn = toggleData.isOn;
			if (toggleData.onValueChanged != null)
			{
				radio.onValueChanged.AddListener(toggleData.onValueChanged);
			}
			return radio;
		}

		public static Slider CreateSlider(SliderData sliderData)
		{
			var slider = Instantiate(instance.sliderPrefab, instance.canvas.transform, false);
			return slider;
		}

		public static Dropdown CreateDropdown(DropdownData dropdownData)
		{
			var dropdown = Instantiate(instance.dropdownPrefab, instance.canvas.transform, false);
			if (dropdownData.options != null)
			{
				dropdown.options = dropdownData.options;
			}
			if (dropdownData.onValueChanged != null)
			{
				dropdown.onValueChanged.AddListener(dropdownData.onValueChanged);
			}
			return dropdown;
		}

		public static InputField CreateInputField(InputFieldData inputFieldData)
		{
			var inputField = Instantiate(instance.inputFieldPrefab, instance.canvas.transform, false);
			if (inputFieldData.onValueChanged != null)
			{
				inputField.onValueChanged.AddListener(inputFieldData.onValueChanged);
			}
			if (inputFieldData.onEndEdit != null)
			{
				inputField.onEndEdit.AddListener(inputFieldData.onEndEdit);
			}
			return inputField;
		}

		public static Window CreateWindow(WindowData windowData)
		{
			var window = Instantiate(instance.windowPrefab, instance.canvas.transform, false);
			window.title.text = windowData.title;
			if (windowData.onUpdate != null)
			{
				window.onUpdate.AddListener(windowData.onUpdate);
			}
			if (windowData.onClose != null)
			{
				window.onClose.AddListener(windowData.onClose);
			}
			return window;
		}

		public static Alert CreateAlert(AlertData alertData)
		{
			var alert = Instantiate(instance.alertPrefab, instance.canvas.transform, false);
			alert.title.text = alertData.title;
			if (alertData.onUpdate != null)
			{
				alert.onUpdate.AddListener(alertData.onUpdate);
			}
			if (alertData.onClose != null)
			{
				alert.onClose.AddListener(alertData.onClose);
			}
			alert.message.text = alertData.message;
			if (alertData.inputs != null)
			{
				foreach (var inputData in alertData.inputs)
				{
					if (inputData is ToggleData)
					{
						var toggleData = (ToggleData)inputData;
						alert.AddToggle(toggleData);
					}
					if (inputData is SliderData)
					{
						var sliderData = (SliderData)inputData;
						alert.AddSlider(sliderData);
					}
					if (inputData is DropdownData)
					{
						var dropdownData = (DropdownData)inputData;
						alert.AddDropdown(dropdownData);
					}
					if (inputData is InputFieldData)
					{
						var inputFieldData = (InputFieldData)inputData;
						alert.AddInputField(inputFieldData);
					}
				}
			}
			if (alertData.buttons != null)
			{
				foreach (var buttonData in alertData.buttons)
				{
					alert.AddButton(buttonData);
				}
			}
			return alert;
		}

		public static ToggleList CreateToggleList(ToggleListData toggleListData)
		{
			var toggleList = Instantiate(instance.toggleListPrefab, instance.canvas.transform, false);
			toggleList.title.text = toggleListData.title;
			if (toggleListData.onUpdate != null)
			{
				toggleList.onUpdate.AddListener(toggleListData.onUpdate);
			}
			if (toggleListData.onClose != null)
			{
				toggleList.onClose.AddListener(toggleListData.onClose);
			}
			if (toggleListData.toggles != null)
			{
				foreach (var toggleData in toggleListData.toggles)
				{
					toggleList.AddToggle(toggleData);
				}
			}
			if (toggleListData.onClickOk != null)
			{
				toggleList.done.onClick.AddListener(toggleListData.onClickOk);
			}
			return toggleList;
		}

		public static RadioList CreateRadioList(ToggleListData radioListData)
		{
			var radioList = Instantiate(instance.radioListPrefab, instance.canvas.transform, false);
			radioList.title.text = radioListData.title;
			if (radioListData.onUpdate != null)
			{
				radioList.onUpdate.AddListener(radioListData.onUpdate);
			}
			if (radioListData.onClose != null)
			{
				radioList.onClose.AddListener(radioListData.onClose);
			}
			if (radioListData.toggles != null)
			{
				foreach (var toggleData in radioListData.toggles)
				{
					radioList.AddToggle(toggleData);
				}
			}
			if (radioListData.onClickOk != null)
			{
				radioList.done.onClick.AddListener(radioListData.onClickOk);
			}
			return radioList;
		}
	}

	public struct ButtonData
	{
		public string text { get; set; }
		public UnityAction onClick { get; set; }
	}

	public interface IInputData
	{

	}

	public struct ToggleData : IInputData
	{
		public string text { get; set; }
		public bool isOn { get; set; }
		public UnityAction<bool> onValueChanged { get; set; }
	}

	public struct SliderData : IInputData
	{

	}

	public struct DropdownData : IInputData
	{
		public List<Dropdown.OptionData> options { get; set; }
		public UnityAction<int> onValueChanged { get; set; }
	}

	public struct InputFieldData : IInputData
	{
		public UnityAction<string> onValueChanged { get; set; }
		public UnityAction<string> onEndEdit { get; set; }
	}

	public interface IWindowData
	{
		string title { get; set; }
		UnityAction<Window> onUpdate { get; set; }
		UnityAction<Window> onClose { get; set; }
	}

	public struct WindowData : IWindowData
	{
		public string title { get; set; }
		public UnityAction<Window> onUpdate { get; set; }
		public UnityAction<Window> onClose { get; set; }
	}

	public struct AlertData : IWindowData
	{
		public string title { get; set; }
		public string message { get; set; }
		public IInputData[] inputs { get; set; }
		public ButtonData[] buttons { get; set; }
		public UnityAction<Window> onUpdate { get; set; }
		public UnityAction<Window> onClose { get; set; }
	}

	public struct ToggleListData : IWindowData
	{
		public string title { get; set; }
		public UnityAction<Window> onUpdate { get; set; }
		public UnityAction<Window> onClose { get; set; }
		public ToggleData[] toggles;
		public UnityAction onClickOk { get; set; }
	}
}