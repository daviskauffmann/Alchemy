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
		Checklist checklistPrefab;

		void Awake()
		{
			instance = this;
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

		public static Checklist CreateChecklist(ChecklistData checklistData)
		{
			var checklist = Instantiate(instance.checklistPrefab, instance.canvas.transform, false);
			checklist.title.text = checklistData.title;
			if (checklistData.onUpdate != null)
			{
				checklist.onUpdate.AddListener(checklistData.onUpdate);
			}
			if (checklistData.onClose != null)
			{
				checklist.onClose.AddListener(checklistData.onClose);
			}
			if (checklistData.toggles != null)
			{
				foreach (var toggleData in checklistData.toggles)
				{
					checklist.AddToggle(toggleData);
				}
			}
			if (checklistData.onClickOk != null)
			{
				checklist.ok.onClick.AddListener(checklistData.onClickOk);
			}
			return checklist;
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

	public struct ChecklistData : IWindowData
	{
		public string title { get; set; }
		public UnityAction<Window> onUpdate { get; set; }
		public UnityAction<Window> onClose { get; set; }
		public ToggleData[] toggles;
		public UnityAction onClickOk { get; set; }
	}
}