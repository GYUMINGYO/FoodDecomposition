using System.Collections.Generic;
using GM.BT;
using GM.Entities;
using GM.Staffs;
using UnityEngine;

namespace GM.UI
{
    public class StatUI : MonoBehaviour, IDescriptableUI
    {
        private Staff _staff;
        private EntityStat _entityStat;
        private List<StatElementUI> _statElementUIList;

        private void Awake()
        {
            _statElementUIList = new List<StatElementUI>(GetComponentsInChildren<StatElementUI>());

        }

        public void InitializeUI(Unit unit)
        {
            _staff = unit as Staff;
            _entityStat = _staff.GetCompo<BTStat>();
            _statElementUIList.ForEach(statElementUI => statElementUI.InitializeUI(_entityStat));
            UpdateUI();
        }

        public void UpdateUI()
        {
            _statElementUIList.ForEach(statElementUI => statElementUI.UpdateUI());
        }
    }
}
