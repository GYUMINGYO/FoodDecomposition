using System.Collections.Generic;
using GM.Entities;
using GM.Staffs;
using UnityEngine;

namespace GM.UI
{
    public class StaffDescriptionUI : MonoBehaviour, IDescriptableUI
    {
        [SerializeField] private PriorityChangeUI _priorityUI;
        [SerializeField] private GameObject _descriptionUI;

        private List<IDescriptableUI> _descriptableElementList;

        private void Awake()
        {
            _descriptableElementList = new List<IDescriptableUI>(_descriptionUI.GetComponentsInChildren<IDescriptableUI>());
        }

        public void InitializeUI(Unit unit)
        {
            Staff staff = unit as Staff;

            if (staff.MyStaffType == StaffType.Waiter)
            {
                // TODO : priorityUI를 움직임 처리
                _priorityUI.InitializeUI(staff as Waiter);
                _priorityUI.gameObject.SetActive(true);
            }
            else
            {
                _priorityUI.gameObject.SetActive(false);
            }

            _descriptableElementList.ForEach(element => element.InitializeUI(unit));
        }

        public void Close()
        {
            _priorityUI.gameObject.SetActive(false);
        }
    }
}
