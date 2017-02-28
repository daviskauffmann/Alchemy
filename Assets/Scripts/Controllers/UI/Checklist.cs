using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Alchemy.Controllers
{
	public class Checklist : Window
	{
		public Transform toggles;
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
			return toggle;
		}
	}
}