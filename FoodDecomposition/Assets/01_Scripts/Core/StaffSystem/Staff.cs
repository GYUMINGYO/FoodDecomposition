using System;
using GM.Data;
using GM.Entities;
using GM.InteractableEntitys;
using GM.Managers;
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
        public StaffInfo Info;

        public StaffType MyStaffType => _myStaffType;
        [SerializeField] private StaffType _myStaffType;

        public Transform FoodHandTrm => _foodHandTrm;
        [SerializeField] protected Transform _foodHandTrm;

        public StaffHandler MyStaffHandler => _staffHandler;
        [SerializeField] protected StaffHandler _staffHandler;

        public bool IsWorking => _isWorking;
        protected bool _isWorking = false;

        public OrderData CurrentData => _currentData;
        protected OrderData _currentData;

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

        public abstract Transform GetTarget(Enums.InteractableEntityType type);
        public abstract void SetIdleState();

        public void StaffInitialize(StaffInfo staffInfo)
        {
            Info = staffInfo;
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

        public virtual void FinishWork()
        {
            _isWorking = false;
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
