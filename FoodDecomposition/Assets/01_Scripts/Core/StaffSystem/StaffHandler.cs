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

        public void Initialize(StaffProfile staffProfile)
        {
            _waiter.StaffInitialize(staffProfile, _level);
            _chef.StaffInitialize(staffProfile, _level);
        }

        private void Update()
        {
            SyncTransform();
        }

        public void SetStaff()
        {
            _waiter.gameObject.SetActive(false);
            _chef.gameObject.SetActive(false);
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
            // TODO : 한바퀴 돌고 이펙트가 나오면서 변신하게 효과 추가하기기
            GetStaff(type, true).gameObject.SetActive(true);
            GetStaff(type).gameObject.SetActive(false);
            GetStaff(type).IsChange = false;
            GetStaff(type).IdleState();
            _type = GetStaff(type, true).MyStaffType;
        }

        public void StartWork()
        {
            GetStaff(_type).gameObject.SetActive(true);
            GetStaff(_type).IdleState();
        }

        public void LeaveWork()
        {
            GetStaff(_type).LeaveWork();
        }

        private void SyncTransform()
        {
            Transform targetTrm = GetStaff(_type).transform;
            GetStaff(_type, true).transform.SetPositionAndRotation(targetTrm.position, targetTrm.rotation);
        }
    }
}
