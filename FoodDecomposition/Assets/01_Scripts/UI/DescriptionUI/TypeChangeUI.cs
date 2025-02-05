using GM.Entities;
using GM.Staffs;
using UnityEngine;

namespace GM.UI
{
    public class TypeChangeUI : MonoBehaviour, IDescriptableUI
    {
        private StaffHandler _staffHandler;

        public void InitializeUI(Unit unit)
        {
            _staffHandler = unit.GetComponentInParent<StaffHandler>();
        }

        // TODO : UI 버튼 대응으로 만든 함수 바꾸기

        public void ChangeTypeToWaiter()
        {
            _staffHandler.StaffChange(StaffType.Waiter);
        }

        public void ChangeTypeToChef()
        {
            _staffHandler.StaffChange(StaffType.Chef);
        }
    }
}
