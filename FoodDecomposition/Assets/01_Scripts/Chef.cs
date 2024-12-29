using GM.Data;
using UnityEngine;

namespace GM.Staffs
{
    public class Chef : Staff
    {
        public ChefState currentWaiterState;
        [SerializeField] private ChefStateChannel _stateChangeEvent;

        protected override void InitializedBT()
        {
            base.InitializedBT();
            _stateChangeEvent = _stateChangeEvent.Clone() as ChefStateChannel;
            _myBTAgent.SetVariableValue("StateChange", _stateChangeEvent);
        }

        public void StartWork(ChefState workType, OrderData data)
        {
            currentWaiterState = workType;
            _currentData = data;
            SetTarget();
            _stateChangeEvent.SendEventMessage(workType);
            _isWorking = true;
        }

        public void SetTarget()
        {
            // TODO : 이거 레시피 경로로 바꿔야 함
            //! 임시 변수
            _myBTAgent.SetVariableValue("MoveTarget", _currentData.orderCustomer.transform);
        }
    }
}
