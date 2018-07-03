using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Alchemy.Controllers {
    public class Window : MonoBehaviour, IBeginDragHandler, IDragHandler {
        [SerializeField]
        private Text title;
        [SerializeField]
        private Button minimize;
        [SerializeField]
        private Button close;
        [SerializeField]
        private RectTransform widthController;
        [SerializeField]
        private RectTransform heightController;
        [SerializeField]
        private GameObject content;
        [SerializeField]
        private WindowEvent onUpdate;
        [SerializeField]
        private WindowEvent onClose;
        private bool active = true;
        private Vector2 offset;

        public float Width {
            get { return widthController.rect.width; }
            set { widthController.sizeDelta = new Vector2(value, widthController.rect.height); }
        }

        public float Height {
            get { return heightController.rect.height; }
            set { heightController.sizeDelta = new Vector2(heightController.rect.width, value); }
        }

        private void Awake() {
            onUpdate = new WindowEvent();
            onClose = new WindowEvent();
        }

        private void Start() {
            close.onClick.AddListener(() => {
                Close();
            });

            minimize.onClick.AddListener(() => {
                Minimize();
            });
        }

        private void Update() {
            if (active) {
                onUpdate.Invoke(this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData) {
            offset = (Vector2)transform.position - eventData.position;
        }

        public void OnDrag(PointerEventData eventData) {
            transform.position = eventData.position + offset;
        }

        public void SetTitle(string text) {
            title.text = text;
        }

        public void AddUpdateListener(UnityAction<Window> call) {
            onUpdate.AddListener(call);
        }

        public void AddCloseListener(UnityAction<Window> call) {
            onClose.AddListener(call);
        }

        public void Minimize() {
            active = !active;

            content.SetActive(active);
        }

        public void Close() {
            onClose.Invoke(this);

            Destroy(gameObject);
        }
    }

    public class WindowEvent : UnityEvent<Window> {

    }
}