using GM.Entities;
using GM.Staffs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GM.UI
{
    public class ProfileUI : MonoBehaviour, IDescriptableUI
    {
        [Header("UI Elements")]
        [SerializeField] private Image _portrait;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _level;

        private Staff _staff;

        public void InitializeUI(Unit unit)
        {
            _staff = unit as Staff;
            _portrait.sprite = _staff.Info.Portrait;
            _name.text = _staff.Info.Name;
        }

        public void UpdateUI()
        {
            // TODO : 레벨 UI Text 정보 추가
            //* 이런거 UI Event를 만들어서 그걸 구독하여 호출하게 하는게 날듯
        }
    }
}
