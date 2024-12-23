using GM.Data;
using Unity.Behavior;
using UnityEngine;

namespace GM.Staffs
{
    public class Waiter : Staff
    {
        public WaiterState currentWaiterState;
        [SerializeField] private StateChange _stateChangeEvent;

        protected override void Awake()
        {
            base.Awake();
            InitializedBT();
        }

        private void InitializedBT()
        {
            _stateChangeEvent = _stateChangeEvent.Clone() as StateChange;
            _myBTAgent.SetVariableValue("StateChange", _stateChangeEvent);
        }

        private void Update()
        {
            //????
            /* if (Input.GetKeyDown(KeyCode.P))
            {
                Customer counterCustomer = ManagerHub.WaiterManager.GetOrderData();
                float sellPrice = counterCustomer.GetSellPrice();
                Debug.Log($"+{sellPrice}");

                counterCustomer.ChangeState(CustomerState.CounterComplete);
            } */
        }


        public void SetTarget()
        {
            _myBTAgent.SetVariableValue("MoveTarget", _currentData.orderCustomer.transform);
        }

        public void StartWork(WaiterState workType, OrderData data)
        {
            currentWaiterState = workType;
            _currentData = data;
            _stateChangeEvent.SendEventMessage(workType);
            _isWorking = true;
        }
    }
}