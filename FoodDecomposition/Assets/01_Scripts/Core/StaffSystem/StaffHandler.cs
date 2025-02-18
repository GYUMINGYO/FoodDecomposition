using System;
using GM.Data;
using GM.GameEventSystem;
using UnityEngine;

namespace GM.Staffs
{
    public class StaffHandler : MonoBehaviour
    {
        public StaffType Type => _type;
        public bool IsChange { get => _isChange; set => _isChange = value; }
        private bool _isChange;

        [Header("Core")]
        [SerializeField] private GameEventChannelSO _restourantCycleChannel;

        [Header("Staff Element")]
        [SerializeField] private StaffType _type = StaffType.Waiter;
        [SerializeField] private Waiter _waiter;
        [SerializeField] private Chef _chef;

        private StaffLevel _level;

        private void Awake()
        {
            _level = GetComponentInChildren<StaffLevel>();

            _restourantCycleChannel.AddListener<RestourantCycleEvent>(HandleRestourantCycle);

            SetStaff();
        }

        public void Initialize(StaffInfo staffInfo)
        {
            _waiter.StaffInitialize(staffInfo, _level);
            _chef.StaffInitialize(staffInfo, _level);
        }

        private void Update()
        {
            SyncTransform();
        }

        private void OnDestroy()
        {
            _restourantCycleChannel.RemoveListener<RestourantCycleEvent>(HandleRestourantCycle);
        }

        private void HandleRestourantCycle(RestourantCycleEvent evt)
        {
            // Close
            if (evt.open == false)
            {
                GetStaff(_type).LeaveWork();
            }
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
