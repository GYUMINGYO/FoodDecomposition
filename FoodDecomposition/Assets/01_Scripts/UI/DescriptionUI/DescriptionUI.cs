using DG.Tweening;
using GM.GameEventSystem;
using UnityEngine;

namespace GM.UI
{
    public class DescriptionUI : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO _unitPickUIEventChannel;

        [Header("UI Setting")]
        [SerializeField] private StaffDescriptionUI _staffDescriptionUI;

        [Header("Show Value")]
        [SerializeField] private float _showDuration = 0.5f;
        [SerializeField] private Ease _showEase = Ease.Linear;
        [SerializeField] private Ease _showOutEase = Ease.Linear;

        private bool _isShow;
        private bool _isMove;
        private RectTransform RectTrm => transform as RectTransform;

        private void Awake()
        {
            _unitPickUIEventChannel.AddListener<UnitDescriptionUIEvent>(HandleUnitDescription);
            _staffDescriptionUI.Close();
            RectTrm.anchoredPosition = new Vector2(-450, RectTrm.anchoredPosition.y);
        }

        private void OnDestroy()
        {
            _unitPickUIEventChannel.RemoveListener<UnitDescriptionUIEvent>(HandleUnitDescription);
        }

        public void UIMove()
        {
            UnitUIEvents.UIDescriptionEvent.UIState = true;
            _unitPickUIEventChannel.RaiseEvent(UnitUIEvents.UIDescriptionEvent);
        }

        public void UIOut()
        {
            UnitUIEvents.UIDescriptionEvent.UIState = false;
            _unitPickUIEventChannel.RaiseEvent(UnitUIEvents.UIDescriptionEvent);
        }

        public void CloseDescription()
        {
            ActiveUI(false);
        }

        private void HandleUnitDescription(UnitDescriptionUIEvent evt)
        {
            ActiveUI(evt.isActive);

            if (evt.isActive == false) return;

            if (evt.type == DescriptionUIType.Unit)
            {
                _staffDescriptionUI.gameObject.SetActive(true);
                _staffDescriptionUI.InitializeUI(evt.unit);
            }
            else
            {
                _staffDescriptionUI.gameObject.SetActive(false);
            }
        }

        private void ActiveUI(bool isActive)
        {
            if (_isShow == isActive || _isMove) return;

            RectTrm.DOKill();
            _isMove = true;
            if (isActive)
            {
                RectTrm.DOAnchorPosX(30, _showDuration).SetEase(_showEase).onComplete += () => _isMove = false;
                _isShow = true;
            }
            else
            {
                RectTrm.DOAnchorPosX(-450, _showDuration).SetEase(_showOutEase).onComplete += () => _isMove = false;
                _staffDescriptionUI.Close();
                _isShow = false;
            }
        }
    }
}
