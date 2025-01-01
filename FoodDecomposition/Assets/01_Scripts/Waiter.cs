using GM.Data;
using UnityEngine;

namespace GM.Staffs
{
    public class Waiter : Staff
    {
        public WaiterState currentWaiterState;
        [SerializeField] private WaiterStateChange _stateChangeEvent;

        protected override void InitializedBT()
        {
            base.InitializedBT();
            _stateChangeEvent = _stateChangeEvent.Clone() as WaiterStateChange;
            _myBTAgent.SetVariableValue("StateChange", _stateChangeEvent);
        }

        public void StartWork(WaiterState workType, OrderData data)
        {
            currentWaiterState = workType;
            _currentData = data;
            SetTarget();
            _stateChangeEvent.SendEventMessage(workType);
            _isWorking = true;
        }

        public void SetTarget()
        {
            _myBTAgent.SetVariableValue("MoveTarget", _currentData.orderCustomer.transform);
        }
    }
}