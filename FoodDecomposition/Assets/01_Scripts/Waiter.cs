using GM.Manager;
using Unity.Behavior;
using UnityEngine;

namespace GM.Staffs
{
    public class Waiter : Staff
    {
        public bool IsWorking => _isWorking;

        private bool _isWorking = false;

        private BehaviorGraphAgent _myBTAgent;
        private BlackboardVariable<StateChange> _myStateChangeEvent;

        void Awake()
        {
            _myBTAgent = GetComponent<BehaviorGraphAgent>();
            _myBTAgent.GetVariable("StateChange", out _myStateChangeEvent);

            Debug.Log(_myStateChangeEvent);
        }

        private void Update()
        {
            //????
            if (Input.GetKeyDown(KeyCode.P))
            {
                Customer counterCustomer = WaiterManager.Instance.GetCounterData();
                float sellPrice = counterCustomer.GetSellPrice();
                Debug.Log($"+{sellPrice}");

                counterCustomer.ChangeState(CustomerState.CounterComplete);
            }
        }

        public void StartWork(WaiterState workType)
        {
            _myStateChangeEvent.Value.SendEventMessage(workType);
        }
    }
}