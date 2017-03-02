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
		public RectTransform widthController;
		public RectTransform heightController;
		public GameObject content;
		public WindowEvent onUpdate;
		public WindowEvent onClose;

		bool active = true;
		Vector2 offset;

		public float Width
		{
			get { return widthController.rect.width; }
			set { widthController.sizeDelta = new Vector2(value, widthController.rect.height); }
		}

		public float Height
		{
			get { return heightController.rect.height; }
			set { heightController.sizeDelta = new Vector2(heightController.rect.width, value); }
		}

		void Awake()
		{
			onUpdate = new WindowEvent();
			onClose = new WindowEvent();
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
			onClose.AddListener((window) =>
			{
				Destroy(gameObject);
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