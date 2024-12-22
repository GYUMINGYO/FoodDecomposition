using GM.Data;
using GM.Managers;
using Unity.Behavior;
using UnityEngine;

namespace GM.Staffs
{
    public class Waiter : Staff
    {
        private BehaviorGraphAgent _myBTAgent;
        [SerializeField] private StateChange _stateChangeEvent;

        private BlackboardVariable<Transform> target;

        protected override void Awake()
        {
            InitializedBT();
        }

        private void InitializedBT()
        {
            _myBTAgent = GetComponent<BehaviorGraphAgent>();
            _stateChangeEvent = _stateChangeEvent.Clone() as StateChange;
            _myBTAgent.SetVariableValue("StateChange", _stateChangeEvent);
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

        private OrderData _currentData;

        public void SetTarget()
        {
            _myBTAgent.SetVariableValue("MoveTarget", _currentData.orderCustomer.transform);
            _myBTAgent.GetVariable("MoveTarget", out target);
            Debug.Log(target.Value.position);
        }

        public void StartWork(WaiterState workType, OrderData data)
        {
            // TODO : Data 처리 해야 함
            _currentData = data;
            _stateChangeEvent.SendEventMessage(workType);
            _isWorking = true;
        }

        public void StartWork(WaiterState workType, Customer data)
        {
            _myBTAgent.SetVariableValue("MoveTarget", data.transform.position);
            _stateChangeEvent.SendEventMessage(workType);
            _isWorking = true;
        }
    }
}