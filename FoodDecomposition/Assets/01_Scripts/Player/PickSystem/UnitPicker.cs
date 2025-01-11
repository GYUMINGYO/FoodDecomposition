using GM.Data;
using GM.Staffs;
using UnityEngine;

namespace GM.Players.Pickers
{
    public class UnitPicker : Picker
    {
        private bool _isPick;

        private Waiter _waiter;

        protected override void PickEntity()
        {
            if (_hit.transform.TryGetComponent(out Waiter waiter))
            {
                _waiter = waiter;
                _isPick = true;
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
                    _waiter.ChangeStatePriority(OrderType.Order);
                    _isPick = false;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    _waiter.ChangeStatePriority(OrderType.Serving);
                    _isPick = false;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    _waiter.ChangeStatePriority(OrderType.Count);
                    _isPick = false;
                }
            }
        }
    }
}
