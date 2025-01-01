using GM.Staffs;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsCompleteCook", story: "[chef] finished cook", category: "Conditions", id: "3e6f5aa2ae9b2130b38bc85171336b97")]
public partial class IsCompleteCookCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Chef> Chef;

    public override bool IsTrue()
    {
        if (Chef.Value.CurrentData.recipe.IsCookingPathComplete())
        {
            // 레시피 항목을 모두 완료했다면 중지
            return true;
        }
        return false;
    }
}
