using DG.Tweening;
using GM.EventSystem;
using UnityEngine;

namespace GM.UI
{
    public class DescriptionUI : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO _unitPickUIEventChannel;

        [Header("UI Setting")]
        [SerializeField]
        private StaffDescriptionUI _staffDescriptionUI;

        [Header("Show Value")]
        [SerializeField] private float _showDuration = 0.5f;
        [SerializeField] private Ease _showEase = Ease.Linear;
        [SerializeField] private Ease _showOutEase = Ease.Linear;

        private bool _isShow;

        private void Awake()
        {
            _unitPickUIEventChannel.AddListener<UnitDescriptionUIEvent>(HandleUnitDescription);
            transform.position = new Vector3(-450, transform.position.y, transform.position.z);
        }

        private void OnDestroy()
        {
            _unitPickUIEventChannel.RemoveListener<UnitDescriptionUIEvent>(HandleUnitDescription);
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
            if (_isShow == isActive) return;

            transform.DOKill();
            if (isActive)
            {
                transform.DOMoveX(50, _showDuration).SetEase(_showEase);
                _isShow = true;
            }
            else
            {
                transform.DOMoveX(-450, _showDuration).SetEase(_showOutEase);
                _isShow = false;
            }
        }
    }
}
