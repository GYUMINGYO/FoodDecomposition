using GM.Entities;
using GM.Staffs;
using UnityEngine;

namespace GM.UI
{
    public class TypeChangeUI : MonoBehaviour, IDescriptableUI
    {
        [SerializeField] private GameObject _priorityChange;
        private StaffHandler _staffHandler;

        public void InitializeUI(Unit unit)
        {
            _staffHandler = unit.GetComponentInParent<StaffHandler>();
        }

        // TODO : UI 버튼 대응으로 만든 함수 바꾸기

        public void ChangeTypeToWaiter()
        {
            _priorityChange.SetActive(true);
            _staffHandler.StaffChange(StaffType.Waiter);
        }

        public void ChangeTypeToChef()
        {
            _priorityChange.SetActive(false);
            _staffHandler.StaffChange(StaffType.Chef);
        }
    }
}
