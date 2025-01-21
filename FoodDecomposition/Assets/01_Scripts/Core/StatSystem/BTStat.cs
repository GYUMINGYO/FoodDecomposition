using AYellowpaper.SerializedCollections;
using GM.Core.StatSystem;
using GM.Entities;
using Unity.Behavior;
using UnityEngine;

namespace GM.BT
{
    public class BTStat : EntityStat
    {
        [SerializeField] protected BehaviorGraphAgent _btAgent;
        [SerializedDictionary("BTVariableName", "Stat")]
        [SerializeField] private SerializedDictionary<string, StatSO> _btStatMappingDictionary;

        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);

            foreach (var mappingStat in _btStatMappingDictionary)
            {
                _btAgent.SetVariableValue<float>(mappingStat.Key, GetStat(mappingStat.Value).Value);
                GetStat(mappingStat.Value).OnValueChange += (StatSO stat, float current, float prev) =>
                {
                    _btAgent.SetVariableValue<float>(mappingStat.Key, current);
                };
            }
        }

        private void OnDestroy()
        {
            foreach (var mappingStat in _btStatMappingDictionary)
            {
                GetStat(mappingStat.Value).OnValueChange = null;
            }
        }
    }
}
