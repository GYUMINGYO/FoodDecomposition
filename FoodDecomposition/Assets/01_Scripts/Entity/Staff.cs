using GM.Data;
using GM.Entities;
using Unity.Behavior;
using UnityEngine;

namespace GM.Staffs
{
    public class Staff : Entity
    {
        // TODO : 직원 스탯
        public bool IsWorking => _isWorking;
        protected bool _isWorking = false;

        public OrderData CurrentData => _currentData;
        protected OrderData _currentData;
        protected BehaviorGraphAgent _myBTAgent;

        protected override void Awake()
        {
            base.Awake();
            InitializedBT();
        }

        protected virtual void InitializedBT()
        {
            _myBTAgent = GetComponent<BehaviorGraphAgent>();
        }

        public void FinishWork()
        {
            _isWorking = false;
        }
    }
}
