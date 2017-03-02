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

		protected bool active = true;

		Vector2 offset;

		public float Width
		{
			get { return GetComponent<RectTransform>().rect.width; }
			set { GetComponent<RectTransform>().sizeDelta = new Vector2(value, Height); }
		}

		public virtual float Height
		{
			get { return content.GetComponent<RectTransform>().rect.height; }
			set { content.GetComponent<RectTransform>().sizeDelta = new Vector2(Width, value); }
		}

		protected virtual void Awake()
		{
			onUpdate = new WindowEvent();
			onClose = new WindowEvent();
		}

		protected virtual void Start()
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

		protected virtual void Update()
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