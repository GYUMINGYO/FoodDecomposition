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
            _isWorking = true;
            _stateChangeEvent.SendEventMessage(workType);
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

        public OrderType[] WorkPriority = { OrderType.Serving, OrderType.Count, OrderType.Order };

        public void ChangeStatePriority(OrderType workType)
        {
            // TODO : WorkPriority 순서 바꾸기 로직
            Debug.Log(workType);
        }
    }
}