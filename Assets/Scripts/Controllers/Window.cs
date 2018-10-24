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
            get { return this.widthController.rect.width; }
            set { this.widthController.sizeDelta = new Vector2(value, this.widthController.rect.height); }
        }

        public float Height {
            get { return this.heightController.rect.height; }
            set { this.heightController.sizeDelta = new Vector2(this.heightController.rect.width, value); }
        }

        private void Awake() {
            this.onUpdate = new WindowEvent();
            this.onClose = new WindowEvent();
        }

        private void Start() {
            this.close.onClick.AddListener(() => {
                this.Close();
            });

            this.minimize.onClick.AddListener(() => {
                this.Minimize();
            });
        }

        private void Update() {
            if (this.active) {
                this.onUpdate.Invoke(this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData) => this.offset = (Vector2)this.transform.position - eventData.position;

        public void OnDrag(PointerEventData eventData) => this.transform.position = eventData.position + this.offset;

        public void SetTitle(string text) => this.title.text = text;

        public void AddUpdateListener(UnityAction<Window> call) => this.onUpdate.AddListener(call);

        public void AddCloseListener(UnityAction<Window> call) => this.onClose.AddListener(call);

        public void Minimize() {
            this.active = !this.active;

            this.content.SetActive(this.active);
        }

        public void Close() {
            this.onClose.Invoke(this);

            Destroy(this.gameObject);
        }
    }

    public class WindowEvent : UnityEvent<Window> {

    }
}
