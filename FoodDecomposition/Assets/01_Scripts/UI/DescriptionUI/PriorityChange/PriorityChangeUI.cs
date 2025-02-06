using GM.Entities;
using GM.GameEventSystem;
using GM.Staffs;
using UnityEngine;

namespace GM.UI
{
    public class PriorityChangeUI : MonoBehaviour, IDescriptableUI
    {
        [SerializeField] private GameEventChannelSO _priorityChangeEvent;
        [SerializeField] private PriorityElementMouseUI _priorityElementMouseUI;

        private PriorityElementUI[] _priorityElementUIs;
        private Waiter _waiter;

        public void InitializeUI(Unit unit)
        {
            if (unit is Waiter)
            {
                _waiter = unit as Waiter;
            }

            _priorityElementUIs = GetComponentsInChildren<PriorityElementUI>();

            for (int i = 0; i < _priorityElementUIs.Length; ++i)
            {
                // TODO : 디버그용 이름 바꾸기 code 삭제하기
                _priorityElementUIs[i].name = $"PriorityElement_{i + 1}";

                _priorityElementUIs[i].InitializeUI(_waiter.WorkPriority[i], _priorityChangeEvent, _priorityElementMouseUI);
            }
        }

        // TODO : 우선순위 변경 UI 구현
    }
}

