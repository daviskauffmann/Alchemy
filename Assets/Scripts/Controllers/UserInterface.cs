using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Alchemy.Controllers
{
	public class UserInterface : MonoBehaviour
	{
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

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.A))
			{
				CreateAlert("Title", "Subtitle", "This is an alert", new InputData[]
				{
					new DropdownData()
					{
						options = new List<Dropdown.OptionData>()
						{
							new Dropdown.OptionData("Option A"),
							new Dropdown.OptionData("Option B"),
							new Dropdown.OptionData("Option C")
						},
						onValueChanged = (value) =>
						{
							Debug.Log(value);
						}
					},
					new InputFieldData()
					{
						onValueChanged = (value) =>
						{
							Debug.Log(value);
						},
						onEndEdit = (value) =>
						{
							Debug.Log(value);
						}
					}
				}, new ButtonData[]
				{
					new ButtonData()
					{
						text = "Button 1",
						onClick = () =>
						{
							Debug.Log("Button 1 Clicked");
						}
					},
					new ButtonData()
					{
						text = "Button 2",
						onClick = () =>
						{
							Debug.Log("Button 2 Clicked");
						}
					}
				}, (window) =>
				{
					Debug.Log("Alert refreshed");
				}, (window) =>
				{
					Debug.Log("Alert closed");
				});
			}

			if (Input.GetKeyDown(KeyCode.E))
			{
				string name = "";
				var alert = CreateAlert("Name", "Enter your name", "", new InputData[]
				{
					new InputFieldData()
					{
						onValueChanged = (value) =>
						{
							name = value;
						},
						onEndEdit = (value) =>
						{
							name = value;
						}
					}
				}, new ButtonData[]
				{
					
				}, (window) =>
				{

				}, (window) =>
				{

				});
				AddButton(alert, new ButtonData()
				{
					text = "Cancel",
					onClick = () =>
					{
						alert.onClose.Invoke(alert);
					}
				});
				AddButton(alert, new ButtonData()
				{
					text = "Ok",
					onClick = () =>
					{
						this.name = name;
						alert.onClose.Invoke(alert);
					}
				});
			}
		}

		public Alert CreateAlert(string title, string subtitle, string message, InputData[] inputs, ButtonData[] buttons, UnityAction<Window> onUpdate, UnityAction<Window> onClose)
		{
			var alert = Instantiate(alertPrefab);
			alert.transform.SetParent(canvas.transform);
			alert.transform.localPosition = Vector2.zero;
			alert.title.text = title;
			alert.subtitle.text = subtitle;
			alert.message.text = message;
			foreach (var inputData in inputs)
			{
				if (inputData is DropdownData)
				{
					var dropdownData = (DropdownData)inputData;
					AddDropdown(alert, dropdownData);
				}
				if (inputData is InputFieldData)
				{
					var inputFieldData = (InputFieldData)inputData;
					AddInputField(alert, inputFieldData);
				}
			}
			foreach (var buttonData in buttons)
			{
				AddButton(alert, buttonData);
			}
			alert.onUpdate.AddListener(onUpdate);
			alert.onClose.AddListener(onClose);
			return alert;
		}

		public Dropdown AddDropdown(Alert alert, DropdownData dropdownData)
		{
			var dropdown = Instantiate(dropdownPrefab);
			dropdown.transform.SetParent(alert.inputs);
			dropdown.options = dropdownData.options;
			dropdown.onValueChanged.AddListener(dropdownData.onValueChanged);
			return dropdown;
		}

		public InputField AddInputField(Alert alert, InputFieldData inputFieldData)
		{
			var inputField = Instantiate(inputFieldPrefab);
			inputField.transform.SetParent(alert.inputs);
			inputField.onValueChanged.AddListener(inputFieldData.onValueChanged);
			inputField.onEndEdit.AddListener(inputFieldData.onEndEdit);
			return inputField;
		}

		public Button AddButton(Alert alert, ButtonData buttonData)
		{
			var button = Instantiate(buttonPrefab);
			button.transform.SetParent(alert.buttons);
			button.GetComponentInChildren<Text>().text = buttonData.text;
			button.onClick.AddListener(buttonData.onClick);
			return button;
		}
	}

	public struct ButtonData
	{
		public string text;
		public UnityAction onClick;
	}

	public interface InputData
	{

	}

	public struct DropdownData : InputData
	{
		public List<Dropdown.OptionData> options;
		public UnityAction<int> onValueChanged;
	}

	public struct InputFieldData : InputData
	{
		public UnityAction<string> onValueChanged;
		public UnityAction<string> onEndEdit;
	}
}