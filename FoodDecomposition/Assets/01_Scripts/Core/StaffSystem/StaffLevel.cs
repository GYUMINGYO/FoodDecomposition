using GM.Entities;
using GM.GameEventSystem;
using GM.Managers;
using UnityEngine;

namespace GM.Staffs
{
    public class StaffLevel : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO _levelUpEventChannel;

        public uint Quality => _quality;
        private uint _quality;

        public uint Level => _level;
        private uint _level;

        public uint NeedLevelUpMoney => _needLevelUpMoney;
        private uint _needLevelUpMoney;

        // TODO  : 레벨 초기화 방법 구현

        private void Awake()
        {
            _level = 1;
            SetNeedLevelUpMoney();
        }

        public void LevelUp()
        {
            float money = ManagerHub.Instance.GetManager<DataManager>().Money;

            if (money < _needLevelUpMoney) return;

            ManagerHub.Instance.GetManager<DataManager>().SubtractMoney(_needLevelUpMoney);
            _level++;
            SetNeedLevelUpMoney();

            UnitUIEvents.LevelUpUIEvent.level = _level;
            UnitUIEvents.LevelUpUIEvent.needLevelUpMoney = _needLevelUpMoney;
            _levelUpEventChannel.RaiseEvent(UnitUIEvents.LevelUpUIEvent);
        }

        private void SetNeedLevelUpMoney()
        {
            // TODO  : 레벨계산 개선
            _needLevelUpMoney = CalculateFibonacci(_level) * (_quality + 2);
        }

        private uint CalculateFibonacci(uint n)
        {
            if (n <= 1) return n;
            uint a = 0;
            uint b = 1;
            for (uint i = 2; i <= n; i++)
            {
                uint temp = a;
                a = b;
                b = temp + b;
            }
            return b;
        }
    }
}
