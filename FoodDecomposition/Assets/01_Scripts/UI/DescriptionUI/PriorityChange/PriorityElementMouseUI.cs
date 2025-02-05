using GM.Data;
using GM.GameEventSystem;
using TMPro;
using UnityEngine;

namespace GM.UI
{
    public class PriorityElementMouseUI : MonoBehaviour
    {
        public OrderType Type => _type;
        private OrderType _type;

        [Header("Setting Value")]
        [SerializeField] private GameEventChannelSO _priorityChangeEvent;
        [SerializeField] private Camera _uiCam;

        [Header("UI Element")]
        [SerializeField] private Transform _visual;
        [SerializeField] private TextMeshProUGUI _name;

        public Rect Rect => _rect;
        private Rect _rect;
        private RectTransform RectTrm => transform as RectTransform;

        private bool _isSelected;

        private void Awake()
        {
            _priorityChangeEvent.AddListener<PriorityUIEvent>(HandlePriorityChange);
            _visual.gameObject.SetActive(false);
            UpdateRect();
        }

        private void HandlePriorityChange(PriorityUIEvent evt)
        {
            _isSelected = evt.isDrag;
            _type = evt.type;
            SetName();
            _visual.gameObject.SetActive(evt.isDrag);
        }

        private void Update()
        {
            if (_isSelected == false) return;

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = _uiCam.farClipPlane;
            Vector3 movement = _uiCam.ScreenToWorldPoint(mousePos);

            transform.position = movement;
            UpdateRect();
        }

        private void UpdateRect()
        {
            _rect = new Rect(
                RectTrm.anchoredPosition.x - RectTrm.sizeDelta.x / 2,
                RectTrm.anchoredPosition.y - RectTrm.sizeDelta.y / 2,
                RectTrm.sizeDelta.x, RectTrm.sizeDelta.y);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(_rect.position, _rect.size);
        }

        public void SetName()
        {
            _name.text = _type.ToString();
        }
    }
}
