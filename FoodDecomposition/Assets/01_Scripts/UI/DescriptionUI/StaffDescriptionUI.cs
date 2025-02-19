using System.Collections.Generic;
using GM.Entities;
using GM.Staffs;
using UnityEngine;

namespace GM.UI
{
    public class StaffDescriptionUI : MonoBehaviour, IDescriptableUI
    {
        [SerializeField] private GameObject _descriptionUI;

        private List<IDescriptableUI> _descriptableElementList;

        private void Awake()
        {
            _descriptableElementList = new List<IDescriptableUI>(_descriptionUI.GetComponentsInChildren<IDescriptableUI>());
        }

        public void InitializeUI(Unit unit)
        {
            Staff staff = unit as Staff;
            _descriptableElementList.ForEach(element => element.InitializeUI(unit));
        }
    }
}
