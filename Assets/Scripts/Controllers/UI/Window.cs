using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Alchemy.Controllers
{
	public class Window : MonoBehaviour, IBeginDragHandler, IDragHandler
	{
		public Text title;
		public Button minimize;
		public Button close;
		public GameObject content;
		public WindowEvent onUpdate;
		public WindowEvent onClose;

		Vector2 offset;
		bool active = true;

		void Awake()
		{
			onUpdate = new WindowEvent();
			onClose = new WindowEvent();

			onClose.AddListener((window) =>
			{
				Destroy(gameObject);
			});
		}

		void Start()
		{
			close.onClick.AddListener(() =>
			{
				onClose.Invoke(this);
			});
			minimize.onClick.AddListener(() =>
			{
				active = !active;
				content.SetActive(active);
			});
		}

		void Update()
		{
			if (active)
			{
				onUpdate.Invoke(this);
			}
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			offset = (Vector2)transform.position - eventData.position;
		}

		public void OnDrag(PointerEventData eventData)
		{
			transform.position = eventData.position + offset;
		}

		public class WindowEvent : UnityEvent<Window>
		{

		}
	}
}