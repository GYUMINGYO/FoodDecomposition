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
            _stateChangeEvent.SendEventMessage(workType);
            _isWorking = true;
        }

        public override Transform GetTarget(Enums.InteractableEntityType type)
        {
            // TODO : Rest는 Staff 타입에 맞게 할당하기
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
                // TODO : 테이블 갖고 와서 테이블 위치로 바꾸기
                return _currentData.orderCustomer.transform;
            }
            else if (type == Enums.InteractableEntityType.FoodOut)
            {
                if (ManagerHub.Instance.GetManager<RestourantManager>().GetInteractableEntity(type, out moveTarget, this))
                {
                    SharedTableEntity foodOut = moveTarget as SharedTableEntity;
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

            if (ManagerHub.Instance.GetManager<RestourantManager>().GetInteractableEntity(type, out moveTarget, this))
            {
                SingleTableEntity singleTableEntity = moveTarget as SingleTableEntity;
                return singleTableEntity.EntityTransform;
            }

            return null;
        }
    }
}