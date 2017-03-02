using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Alchemy.Controllers
{
	public class RadioList : Window
	{
		public Transform toggles;
		public ToggleGroup toggleGroup;
		public Button done;
		public UnityEvent onClickDone;

		protected override void Start()
		{
			base.Start();
			done.onClick.AddListener(() =>
			{
				onClose.Invoke(this);
			});
		}

		public Toggle AddToggle(ToggleData toggleData)
		{
			var toggle = UserInterface.CreateRadio(toggleData);
			toggle.transform.SetParent(toggles);
			toggle.group = toggleGroup;
			return toggle;
		}
	}
}