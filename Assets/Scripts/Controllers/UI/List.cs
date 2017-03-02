using UnityEngine;
using UnityEngine.UI;

namespace Alchemy.Controllers
{
	public class List : Window
	{
		public Transform elements;
		public Transform buttons;

		public Toggle AddToggle(ToggleData toggleData)
		{
			var toggle = UserInterface.CreateToggle(toggleData);
			toggle.transform.SetParent(elements);
			return toggle;
		}

		public Toggle AddRadio(RadioData radioData)
		{
			var radio = UserInterface.CreateRadio(radioData);
			radio.transform.SetParent(elements);
			var group = elements.GetComponent<ToggleGroup>();
			if (group == null)
			{
				radio.group = elements.gameObject.AddComponent<ToggleGroup>();
			}
			else
			{
				radio.group = group;
			}
			return radio;
		}

		public Button AddButton(ButtonData buttonData)
		{
			var button = UserInterface.CreateButton(buttonData);
			button.transform.SetParent(buttons);
			return button;
		}
	}
}