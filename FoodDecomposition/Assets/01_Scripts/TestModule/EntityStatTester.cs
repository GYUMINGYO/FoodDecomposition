using GM.Core.StatSystem;
using GM.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GM.Test
{
    public class EntityStatTester : MonoBehaviour
    {
        [SerializeField] private EntityStat _statCompo;
        [SerializeField] private StatSO _targetStat;
        [SerializeField] private float _testValue;

        private void Update()
        {
            if (Keyboard.current.qKey.wasPressedThisFrame)
                _statCompo.AddModifier(_targetStat, this, _testValue);

            if (Keyboard.current.eKey.wasPressedThisFrame)
                _statCompo.RemoveModifier(_targetStat, this);
        }
    }
}
