using GM.Data;
using GM.InteractableEntitys;
using GM.Managers;
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
            _stateChangeEvent.SendEventMessage(workType);
            _isWorking = true;
        }

        public override Transform GetTarget(Enums.InteractableEntityType type)
        {
            // TODO : Rest는 Staff 타입에 맞게 할당하기
            InteractableEntity moveTarget;

            if (type == Enums.InteractableEntityType.Rest)
            {
                RestRoom chefRest;
                if (ManagerHub.Instance.GetManager<RestourantManager>().GetRestEntity(type, out chefRest, this, StaffType.Chef))
                {
                    return chefRest.EntityTransform;
                }
            }
            else if (type == Enums.InteractableEntityType.FoodOut)
            {
                if (ManagerHub.Instance.GetManager<RestourantManager>().GetInteractableEntity(type, out moveTarget, this))
                {
                    SharedTableEntity foodOut = moveTarget as SharedTableEntity;
                    return foodOut.SenderTransform;
                }
            }
            else if (type == Enums.InteractableEntityType.Recipe)
            {
                return _currentData.recipe.GetNextCookingTable(this).transform;
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
