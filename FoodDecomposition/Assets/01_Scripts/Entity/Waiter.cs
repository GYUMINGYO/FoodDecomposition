using System;
using GM.Data;
using GM.InteractableEntitys;
using GM.Managers;
using UnityEngine;

namespace GM.Staffs
{
    public class Waiter : Staff
    {
        public WaiterState currentWaiterState;
        [SerializeField] private WaiterStateChange _stateChangeEvent;

        private bool _isFoodTrash;

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
            _stateChangeEvent.SendEventMessage(workType);
            _currentData.orderCustomer.customerExitEvent += HandleExitEvent;
            _isWorking = true;
        }

        private void Update()
        {
            if (_isFoodTrash == true)
            {
                Debug.Log("음식 버리러 가기");
                _stateChangeEvent.SendEventMessage(WaiterState.FoodTrash);
                _isFoodTrash = false;
            }
        }

        public override void FinishWork()
        {
            if (_currentData.orderCustomer != null)
            {
                _currentData.orderCustomer.customerExitEvent -= HandleExitEvent;
            }
            base.FinishWork();
        }

        public override Transform GetTarget(Enums.InteractableEntityType type)
        {
            InteractableEntity moveTarget;

            if (type == Enums.InteractableEntityType.Rest)
            {
                RestRoom waiterRest;
                if (ManagerHub.Instance.GetManager<RestourantManager>().GetRestEntity(type, out waiterRest, this, StaffType.Waiter))
                {
                    return waiterRest.EntityTransform;
                }
            }
            else if (type == Enums.InteractableEntityType.Order)
            {
                if (_currentData.orderCustomer.IsOut == true)
                {
                    _stateChangeEvent.SendEventMessage(WaiterState.FoodTrash);
                }
                _myBTAgent.SetVariableValue("OrderCustomerTrm", _currentData.orderCustomer.transform);
                return _currentData.orderTable.GetWaiterStandTrm(transform);
            }
            else if (type == Enums.InteractableEntityType.FoodOut)
            {
                if (ManagerHub.Instance.GetManager<RestourantManager>().GetInteractableEntity(type, out moveTarget, this))
                {
                    FoodOut foodOut = moveTarget as FoodOut;
                    _myBTAgent.SetVariableValue("FoodTrm", foodOut.FoodTrm);
                    _myBTAgent.SetVariableValue("TableFoodTrm", _currentData.orderTable.GetFoodPos(_currentData.orderCustomer));
                    return foodOut.ReceiverTransform;
                }
            }
            else if (type == Enums.InteractableEntityType.Counter)
            {
                if (ManagerHub.Instance.GetManager<RestourantManager>().GetInteractableEntity(type, out moveTarget, this))
                {
                    SingleCounterEntity foodOut = moveTarget as SingleCounterEntity;
                    return foodOut.SenderTransform;
                }
            }
            else if (type == Enums.InteractableEntityType.FoodTrashContainer)
            {
                if (ManagerHub.Instance.GetManager<RestourantManager>().GetInteractableEntity(type, out moveTarget, this))
                {
                    FoodTrashContainer foodTrash = moveTarget as FoodTrashContainer;
                    _myBTAgent.SetVariableValue("FoodTrashTrm", foodTrash.FoodTrashTransform);
                    _isWorking = true;
                    return foodTrash.EntityTransform;
                }
            }

            if (ManagerHub.Instance.GetManager<RestourantManager>().GetInteractableEntity(type, out moveTarget, this))
            {
                SingleTableEntity singleTableEntity = moveTarget as SingleTableEntity; 
                return singleTableEntity.EntityTransform;
            }

            return null;
        }

        private void HandleExitEvent()
        {
            Debug.Log("손님 나감");
            _isFoodTrash = true;
        }
    }
}