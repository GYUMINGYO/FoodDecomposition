using GM.Data;
using GM.Managers;
using Unity.Behavior;
using UnityEngine;

namespace GM.Staffs
{
    public class Waiter : Staff
    {
        private BehaviorGraphAgent _myBTAgent;
        private BlackboardVariable<StateChange> _myStateChangeEvent;

        protected override void Awake()
        {
            InitializedBT();
        }

        private void InitializedBT()
        {
            _myBTAgent = GetComponent<BehaviorGraphAgent>();
            _myBTAgent.GetVariable("StateChange", out _myStateChangeEvent);
        }

        private void Update()
        {
            //????
            if (Input.GetKeyDown(KeyCode.P))
            {
                Customer counterCustomer = ManagerHub.WaiterManager.GetCounterData();
                float sellPrice = counterCustomer.GetSellPrice();
                Debug.Log($"+{sellPrice}");

                counterCustomer.ChangeState(CustomerState.CounterComplete);
            }
        }

        public void StartWork(WaiterState workType, OrderData data)
        {
            _myStateChangeEvent.Value.SendEventMessage(workType);
        }
    }
}