using GM.Data;
using GM.InteractableEntities;
using GM.Managers;
using UnityEngine;

namespace GM.Staffs
{
    public class Waiter : Staff
    {
        public OrderType[] WorkPriority;
        public WaiterState currentWaiterState;

        [SerializeField] private WaiterStateChange _stateChangeEvent;

        protected override void InitializedBT()
        {
            base.InitializedBT();
            WorkPriority = new OrderType[3] { OrderType.Serving, OrderType.Count, OrderType.Order };
            _stateChangeEvent = _stateChangeEvent.Clone() as WaiterStateChange;
            SetVariable("StateChange", _stateChangeEvent);
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
                    SetTable(waiterRest);
                    return waiterRest.EntityTransform;
                }
            }
            else if (type == Enums.InteractableEntityType.Order)
            {
                SetVariable("OrderCustomerTrm", _currentData.orderCustomer.transform);
                return _currentData.orderTable.GetWaiterStandTrm(transform);
            }
            else if (type == Enums.InteractableEntityType.FoodOut)
            {
                if (ManagerHub.Instance.GetManager<RestourantManager>().GetInteractableEntity(type, out moveTarget, this))
                {
                    FoodOut foodOut = moveTarget as FoodOut;
                    SetTable(foodOut);
                    SetVariable("FoodTrm", foodOut.FoodTrm);
                    SetVariable("TableFoodTrm", _currentData.orderTable.GetFoodPos(_currentData.orderCustomer));
                    return foodOut.ReceiverTransform;
                }
            }
            else if (type == Enums.InteractableEntityType.Counter)
            {
                if (ManagerHub.Instance.GetManager<RestourantManager>().GetInteractableEntity(type, out moveTarget, this))
                {
                    SingleCounterEntity foodOut = moveTarget as SingleCounterEntity;
                    SetTable(foodOut);
                    return foodOut.SenderTransform;
                }
            }
            else if (type == Enums.InteractableEntityType.FoodTrashContainer)
            {
                if (ManagerHub.Instance.GetManager<RestourantManager>().GetInteractableEntity(type, out moveTarget, this))
                {
                    FoodTrashContainer foodTrash = moveTarget as FoodTrashContainer;
                    SetTable(foodTrash);
                    SetVariable("FoodTrashTrm", foodTrash.FoodTrashTransform);
                    _isWorking = true;
                    return foodTrash.EntityTransform;
                }
            }
            else if (type == Enums.InteractableEntityType.Exit)
            {
                if (ManagerHub.Instance.GetManager<RestourantManager>().GetInteractableEntity(type, out moveTarget, this))
                {
                    SingleTableEntity exit = moveTarget as SingleTableEntity;
                    return exit.EntityTransform;
                }
            }
            else if (ManagerHub.Instance.GetManager<RestourantManager>().GetInteractableEntity(type, out moveTarget, this))
            {
                SingleTableEntity singleTableEntity = moveTarget as SingleTableEntity;
                SetTable(singleTableEntity);
                return singleTableEntity.EntityTransform;
            }

            return null;
        }

        public override void SetIdleState()
        {
            _stateChangeEvent.SendEventMessage(WaiterState.IDLE);
            StaffHandlerBoolChange();
        }

        public void ChangeStatePriority(OrderType[] workTypeArr)
        {
            if (workTypeArr.Length > 3) return;

            WorkPriority = workTypeArr;
        }

        public override void LeaveWork()
        {
            _stateChangeEvent.SendEventMessage(WaiterState.LeaveWork);
        }
    }
}