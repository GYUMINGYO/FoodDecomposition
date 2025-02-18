using GM.Data;
using GM.Entities;
using GM.InteractableEntities;
using Unity.Behavior;
using UnityEngine;

namespace GM.Staffs
{
    public enum StaffType
    {
        Waiter,
        Chef
    }

    public abstract class Staff : Unit
    {
        public StaffProfile Profile;

        public StaffType MyStaffType => _myStaffType;
        [SerializeField] private StaffType _myStaffType;

        public Transform FoodHandTrm => _foodHandTrm;
        [SerializeField] protected Transform _foodHandTrm;

        public StaffHandler MyStaffHandler => _staffHandler;
        [SerializeField] protected StaffHandler _staffHandler;

        public StaffLevel Level => _level;
        protected StaffLevel _level;

        public OrderData CurrentData => _currentData;
        protected OrderData _currentData;

        public bool IsWorking => _isWorking;
        protected bool _isWorking = false;

        public bool IsChange { get => _isChange; set => _isChange = value; }
        private bool _isChange;

        protected BehaviorGraphAgent _myBTAgent;
        protected InteractableEntity _targetTable;

        protected override void Awake()
        {
            base.Awake();
            InitializedBT();
        }

        protected virtual void InitializedBT()
        {
            _myBTAgent = GetComponent<BehaviorGraphAgent>();
        }

        // TODO : GetTarget 방식 바꾸기
        // if문이 너무 길고
        // 타입이 늘어날 수록 예외 처리가 너무 많아짐
        // 간단해서 좋지만 좋은 구조는 아님

        public abstract Transform GetTarget(Enums.InteractableEntityType type);
        public abstract void SetIdleState();
        public abstract void LeaveWork();

        public virtual void FinishWork()
        {
            _isWorking = false;
        }

        public void StaffInitialize(StaffProfile staffProfile, StaffLevel staffLevel)
        {
            Profile = staffProfile;
            _level = staffLevel;
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

        public void StaffChangeEvent()
        {
            _staffHandler.ChangeProcess(_myStaffType);
            _isChange = false;
        }

        public void StaffHandlerBoolChange()
        {
            _staffHandler.IsChange = false;
        }

        public void SetTable(InteractableEntity table)
        {
            if (table == null) return;

            _targetTable = table;
            _targetTable.InUse = true;
        }

        public void EndTable()
        {
            if (_targetTable == null) return;

            _targetTable.InUse = false;
        }
    }
}
