using System;
using GM.Entities;
using GM.GameEventSystem;
using GM.Staffs;
using TMPro;
using UnityEngine;

namespace GM.UI
{
    public class UpgradeUI : MonoBehaviour, IDescriptableUI
    {
        [SerializeField] private GameEventChannelSO _levelUpEventChannel;
        [SerializeField] private TextMeshProUGUI _price;

        private Staff _staff;
        private StaffLevel _staffLevel;

        private void Awake()
        {
            _levelUpEventChannel.AddListener<LevelUpUIEvent>(HandleLevelUpEvent);
        }

        private void HandleLevelUpEvent(LevelUpUIEvent evt)
        {
            _price.SetText($"Price : {evt.needLevelUpMoney}");
        }

        private void OnDestroy()
        {
            _levelUpEventChannel.RemoveListener<LevelUpUIEvent>(HandleLevelUpEvent);
        }

        public void InitializeUI(Unit unit)
        {
            _staff = unit as Staff;
            _staffLevel = _staff.Level;

            _price.SetText($"Price : {_staffLevel.NeedLevelUpMoney}");
        }

        public void LevelUp()
        {
            // TODO : 현재 돈에 따라서 UI 표시 다르게 하기
            _staffLevel.LevelUp();
        }
    }
}
