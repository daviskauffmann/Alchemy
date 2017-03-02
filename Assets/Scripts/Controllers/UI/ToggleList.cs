using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Alchemy.Controllers
{
	public class ToggleList : Window
	{
		public Transform toggles;
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
			var toggle = UserInterface.CreateToggle(toggleData);
			toggle.transform.SetParent(toggles);
			return toggle;
		}
	}
}