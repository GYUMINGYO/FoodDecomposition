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

        protected BehaviorGraphAgent _myBTAgent;
        protected OrderData _currentData;
        protected Transform _waitTrm;

        protected override void Awake()
        {
            base.Awake();

            _myBTAgent = GetComponent<BehaviorGraphAgent>();
        }

        public void FinishWork()
        {
            _myBTAgent.SetVariableValue("MoveTarget", _waitTrm);
            _isWorking = false;
        }
    }
}
