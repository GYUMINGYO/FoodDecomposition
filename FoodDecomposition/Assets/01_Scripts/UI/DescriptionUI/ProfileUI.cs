using System;
using GM.Entities;
using GM.EventSystem;
using GM.Staffs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GM.UI
{
    public class ProfileUI : MonoBehaviour, IDescriptableUI
    {
        [SerializeField] private GameEventChannelSO _levelUpEventChannel;

        [Header("UI Elements")]
        [SerializeField] private Image _portrait;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _level;

        private Staff _staff;
        private StaffLevel _staffLevel;

        private void Awake()
        {
            _levelUpEventChannel.AddListener<LevelUpUIEvent>(HandleLevelUp);
        }

        private void HandleLevelUp(LevelUpUIEvent evt)
        {
            _level.SetText($"LEVEL : {evt.level}");
        }

        private void OnDestroy()
        {
            _levelUpEventChannel.RemoveListener<LevelUpUIEvent>(HandleLevelUp);
        }

        public void InitializeUI(Unit unit)
        {
            _staff = unit as Staff;
            _staffLevel = _staff.Level;

            _portrait.sprite = _staff.Info.Portrait;
            _name.text = _staff.Info.Name;
            _level.SetText($"LEVEL : {_staffLevel.Level}");
            // TODO : Level Quality에 따른 색상 변경
        }
    }
}
