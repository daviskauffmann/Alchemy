using UnityEngine;
using UnityEngine.EventSystems;

namespace Alchemy.Views
{
    public class Window : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        Vector2 _offset;
        [SerializeField]Transform _content = null;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _offset = (Vector2)transform.position - eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position + _offset;
        }

        public void Open()
        {
            GetComponent<CanvasGroup>().alpha = 1;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        public void Minimize()
        {
            if (_content.GetComponent<CanvasGroup>().alpha == 0)
            {
                _content.GetComponent<CanvasGroup>().alpha = 1;
                _content.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            else
            {
                _content.GetComponent<CanvasGroup>().alpha = 0;
                _content.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }

        public void Close()
        {
            GetComponent<CanvasGroup>().alpha = 0;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
}