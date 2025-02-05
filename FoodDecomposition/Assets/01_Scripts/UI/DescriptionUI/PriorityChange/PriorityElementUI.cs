using DG.Tweening;
using GM.Data;
using GM.GameEventSystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GM.UI
{
    public class PriorityElementUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public OrderType Type => _type;
        [SerializeField] private OrderType _type;

        [Header("UI Element")]
        [SerializeField] private Image _visual;
        [SerializeField] private TextMeshProUGUI _name;

        private GameEventChannelSO _priorityChangeEvent;
        private LayoutElement _layoutElement;
        private PriorityElementMouseUI _priorityMouseUI;
        private Rect _rect;
        private RectTransform RectTrm => transform as RectTransform;
        private bool _isShow = true;
        private bool _isDetected;

        private void Awake()
        {
            _layoutElement = GetComponent<LayoutElement>();
        }

        public void InitializeUI(OrderType type, GameEventChannelSO priorityChangeEvent, PriorityElementMouseUI priorityMouseUI)
        {
            _type = type;
            _priorityChangeEvent = priorityChangeEvent;
            _priorityMouseUI = priorityMouseUI;
            UpdateRect();
            _isShow = true;
            SetName();
        }

        private void Update()
        {
            if (_isShow) return;

            if (_rect.Overlaps(_priorityMouseUI.Rect))
            {
                Detected();
            }
            else
            {
                NotDetected();
            }
        }

        public void SetName()
        {
            _name.text = _type.ToString();
        }

        public void Open()
        {
            DOTween.Kill(this);
            _layoutElement.flexibleHeight = 1;
            _visual.gameObject.SetActive(true);
            _isShow = true;
        }

        public void Close()
        {
            DOTween.Kill(this);
            _visual.gameObject.SetActive(false);
            DOTween.To(() => _layoutElement.flexibleHeight, x => _layoutElement.flexibleHeight = x, 0f, 0.25f).SetEase(Ease.OutExpo);
            _isShow = false;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            UnitUIEvents.PriorityUIEvent.isDrag = true;
            UnitUIEvents.PriorityUIEvent.type = _type;
            _priorityChangeEvent.RaiseEvent(UnitUIEvents.PriorityUIEvent);
            Close();
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            UnitUIEvents.PriorityUIEvent.isDrag = false;
            _priorityChangeEvent.RaiseEvent(UnitUIEvents.PriorityUIEvent);
            Open();
        }

        private void Detected()
        {
            if (_isDetected) return;

            _isDetected = true;

            Debug.Log("감지");

            DOTween.Kill(this);
            DOTween.To(() => _layoutElement.flexibleHeight, x => _layoutElement.flexibleHeight = x, 1f, 0.25f).SetEase(Ease.OutExpo);
        }

        private void NotDetected()
        {
            if (_isDetected == false) return;

            _isDetected = false;

            Debug.Log("감지 되지 않음");

            DOTween.Kill(this);
            DOTween.To(() => _layoutElement.flexibleHeight, x => _layoutElement.flexibleHeight = x, 0f, 0.25f).SetEase(Ease.OutExpo);
        }

        private void UpdateRect()
        {
            _rect = new Rect(
                RectTrm.anchoredPosition.x,
                RectTrm.anchoredPosition.y - RectTrm.sizeDelta.y,
                RectTrm.sizeDelta.x, RectTrm.sizeDelta.y);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(_rect.center, _rect.size);
        }
    }
}
