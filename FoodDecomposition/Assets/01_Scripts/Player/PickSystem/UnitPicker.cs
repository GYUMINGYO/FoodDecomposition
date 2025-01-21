using GM.Staffs;
using UnityEngine;

namespace GM.Players.Pickers
{
    public class UnitPicker : Picker
    {
        private bool _isPick;

        private Staff _staff;

        protected override void PickEntity()
        {
            if (_hit.transform.TryGetComponent(out Staff staff))
            {
                _staff = staff;
                _isPick = true;
                // TODO : 지금은 유닛 직업 변경임
                // TODO : WorkPriority 순서 바꾸기 로직 처리하기
            }
        }

        //! Test
        private void Update()
        {
            if (_isPick)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    _staff.MyStaffHandler.StaffChange(StaffType.Waiter);
                    //_waiter.ChangeStatePriority(OrderType.Order);
                    _isPick = false;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    _staff.MyStaffHandler.StaffChange(StaffType.Chef);
                    //_waiter.ChangeStatePriority(OrderType.Serving);
                    _isPick = false;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    //_waiter.ChangeStatePriority(OrderType.Count);
                    _isPick = false;
                }
            }
        }
    }
}
