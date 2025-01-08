using GM.Data;
using GM.Entities;
using Unity.Behavior;
using UnityEngine;

namespace GM.Staffs
{
    public enum StaffType
    {
        Waiter,
        Chef
    }

    public abstract class Staff : Entity
    {
        // TODO : 직원 스탯
        public bool IsWorking => _isWorking;
        protected bool _isWorking = false;

        public OrderData CurrentData => _currentData;
        protected OrderData _currentData;
        
        public Transform FoodHandTrm => _foodHandTrm;
        [SerializeField] protected Transform _foodHandTrm;
        
        protected BehaviorGraphAgent _myBTAgent;

        protected override void Awake()
        {
            base.Awake();
            InitializedBT();
        }

        protected virtual void InitializedBT()
        {
            _myBTAgent = GetComponent<BehaviorGraphAgent>();
        }

        public BlackboardVariable<T> GetVariable<T>(string variableName)
        {
            if (_myBTAgent.GetVariable(variableName, out BlackboardVariable<T> variable))
            {
                return variable;
            }
            return null;
        }

        public void SetVariable<T>(string variableName, T value)
        {
            BlackboardVariable<T> variable = GetVariable<T>(variableName);
            Debug.Assert(variable != null, $"Variable {variableName} not found");
            variable.Value = value;
        }

        public void FinishWork()
        {
            _isWorking = false;
        }

        public abstract Transform GetTarget(Enums.InteractableEntityType type);
    }
}
