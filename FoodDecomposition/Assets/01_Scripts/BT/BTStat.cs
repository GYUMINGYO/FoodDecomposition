using GM.Core.StatSystem;
using GM.Entities;
using Unity.Behavior;
using UnityEngine;

namespace GM.BT
{
    public abstract class BTStat : EntityStat
    {
        [SerializeField] protected BehaviorGraphAgent _btAgent;
        [SerializeField] protected StatSO _movementSpeedStat;

        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);
            SetStats();
        }

        protected abstract void SetStats();

        protected void SetBTValue(string valueName, StatSO stat)
        {
            _btAgent.SetVariableValue<float>(valueName, stat.Value);
        }

        protected void SetBTValue(string valueName, float value)
        {
            _btAgent.SetVariableValue<float>(valueName, value);
        }
    }
}
