using GM.BT;
using UnityEngine;

namespace GM.Core.StatSystem
{
    public class ChefStat : BTStat
    {
        // TODO : Stat과 BTValue 맵핑을 딕셔너리로 바꾸기

        [SerializeField] private StatSO _cookingSpeedStat;

        protected override void SetStats()
        {
            GetStat(_movementSpeedStat).OnValueChange += HandleMoveSpeed;
            GetStat(_cookingSpeedStat).OnValueChange += HandleCookingSpeed;

            SetBTValue("MoveSpeed", GetStat(_movementSpeedStat));
            SetBTValue("CookingSpeed", GetStat(_cookingSpeedStat));
        }

        private void OnDestroy()
        {
            GetStat(_movementSpeedStat).OnValueChange -= HandleMoveSpeed;
            GetStat(_cookingSpeedStat).OnValueChange -= HandleCookingSpeed;
        }

        private void HandleMoveSpeed(StatSO stat, float current, float prev) => SetBTValue("MoveSpeed", current);
        private void HandleCookingSpeed(StatSO stat, float current, float prev) => SetBTValue("CookingSpeed", current);

    }
}
