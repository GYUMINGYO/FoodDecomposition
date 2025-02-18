using GM.CookWare;
using GM.Data;
using GM.InteractableEntities;
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
            SetVariable("StateChange", _stateChangeEvent);
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
            InteractableEntity moveTarget;

            if (type == Enums.InteractableEntityType.Rest)
            {
                RestRoom chefRest;
                if (ManagerHub.Instance.GetManager<RestourantManager>().GetRestEntity(type, out chefRest, this, StaffType.Chef))
                {
                    SetTable(chefRest);
                    return chefRest.EntityTransform;
                }
            }
            else if (type == Enums.InteractableEntityType.FoodOut)
            {
                if (ManagerHub.Instance.GetManager<RestourantManager>().GetInteractableEntity(type, out moveTarget, this))
                {
                    FoodOut foodOut = moveTarget as FoodOut;
                    SetVariable("FoodTrm", foodOut.FoodTrm);
                    return foodOut.SenderTransform;
                }
            }
            else if (type == Enums.InteractableEntityType.Recipe)
            {
                CookingTable cookingTable = _currentData.recipe.GetNextCookingTable(this);
                if (cookingTable == null) return null;
                SetTable(cookingTable);
                return cookingTable.EntityTransform;
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
            _stateChangeEvent.SendEventMessage(ChefState.IDLE);
            StaffHandlerBoolChange();
        }

        public override void LeaveWork()
        {
            _stateChangeEvent.SendEventMessage(ChefState.LeaveWork);
        }
    }
}
