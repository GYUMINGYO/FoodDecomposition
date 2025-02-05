using System;
using GM.Core.StatSystem;
using GM.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GM.UI
{
    public class StatElementUI : MonoBehaviour
    {
        [SerializeField] private StatSO _statSOType;

        [Header("UI Element")]
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _statInfo;

        private EntityStat _entityStat;
        private StatSO _stat;

        public void InitializeUI(EntityStat entityStat)
        {
            _entityStat = entityStat;
            if (_stat != null)
            {
                _stat.OnValueChange -= HandleValueChange;
            }
            _stat = _entityStat.GetStat(_statSOType);

            _icon.sprite = _stat.icon;
            UpdateUI();

            _stat.OnValueChange += HandleValueChange;
        }

        public void UpdateUI()
        {
            // TODO : 스텟당 소수점 자리수를 다르게 표시할 수 있도록 수정
            _statInfo.SetText($"{_stat.statName} : {_stat.Value.ToString("F2")}");
        }

        private void HandleValueChange(StatSO stat, float current, float prev)
        {
            UpdateUI();
        }
    }
}
