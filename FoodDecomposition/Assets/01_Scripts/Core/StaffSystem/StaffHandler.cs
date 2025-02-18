using System.Collections;
using GM.Data;
using UnityEngine;

namespace GM.Staffs
{
    public class StaffHandler : MonoBehaviour
    {
        public StaffType Type => _type;
        public bool IsChange { get => _isChange; set => _isChange = value; }
        private bool _isChange;

        [Header("Staff Element")]
        [SerializeField] private StaffType _type = StaffType.Waiter;
        [SerializeField] private Waiter _waiter;
        [SerializeField] private Chef _chef;

        private StaffLevel _level;

        private void Awake()
        {
            _level = GetComponentInChildren<StaffLevel>();
            SetStaff();
        }

        public void Initialize(StaffProfile staffInfo)
        {
            _waiter.StaffInitialize(staffInfo, _level);
            _chef.StaffInitialize(staffInfo, _level);
        }

        private void Update()
        {
            SyncTransform();
        }

        public void LeaveWork()
        {
            GetStaff(_type).LeaveWork();
        }

        public void SetStaff()
        {
            GetStaff(_type, true).gameObject.SetActive(false);
        }

        public Staff GetStaff(StaffType type, bool inverse = false)
        {
            if (inverse)
            {
                type -= 1;
            }
            return type == StaffType.Waiter ? _waiter : _chef;
        }

        public void StaffChange(StaffType type)
        {
            if (_type == type) return;

            _isChange = true;
            GetStaff(type, true).IsChange = true;
        }

        public void ChangeProcess(StaffType type)
        {
            // TODO : Change 함수에 변경 효과 추가하기
            GetStaff(type, true).gameObject.SetActive(true);
            GetStaff(type).gameObject.SetActive(false);
            _type = GetStaff(type, true).MyStaffType;
            GetStaff(type, true).SetIdleState();
        }

        private void SyncTransform()
        {
            Transform targetTrm = GetStaff(_type).transform;
            GetStaff(_type, true).transform.SetPositionAndRotation(targetTrm.position, targetTrm.rotation);
        }
    }
}
