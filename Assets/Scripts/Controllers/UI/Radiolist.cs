using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Alchemy.Controllers
{
	public class Radiolist : Window
	{
		public Transform toggles;
		public ToggleGroup toggleGroup;
		public Button ok;
		public UnityEvent onClickOk;

		protected override void Start()
		{
			base.Start();
			ok.onClick.AddListener(() =>
			{
				onClose.Invoke(this);
			});
		}

		public Toggle AddToggle(ToggleData toggleData)
		{
			var toggle = UserInterface.CreateToggle(toggleData);
			toggle.transform.SetParent(toggles);
			toggle.group = toggleGroup;
			return toggle;
		}
	}
}