using GM.Staffs;
using System;
using GM.CookWare;
using GM.Entities;
using GM.InteractableEntities;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using GM;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetRecipeAnimationAction", story: "set current recipe animation of [chef] with [entityAnimator] and [cookingSpeed]", category: "Action", id: "613fc65ead7fe9a85ab296ae6303362f")]
public partial class SetRecipeAnimationActionAction : Action
{
    [SerializeReference] public BlackboardVariable<Chef> Chef;
    [SerializeReference] public BlackboardVariable<EntityAnimator> EntityAnimator;
    [SerializeReference] public BlackboardVariable<float> CookingSpeed;

    private ChefAnimator _chefAnimator;
    private CookingTable _cookingTable;

    protected override Status OnStart()
    {
        _chefAnimator = EntityAnimator.Value as ChefAnimator;

        // TODO : 냉장고 같이 플레이어 움직임이 필요한 동작 해결하기
        _cookingTable = Chef.Value.CurrentData.recipe.GetCurrentCookingTable();
        if (_cookingTable is Refrigerator refrigerator)
        {
            refrigerator.SetChef(Chef.Value);
        }

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        _chefAnimator.SetCookingAnimation(_cookingTable.CookAnimation, CookingSpeed.Value);
        return Status.Success;
    }
}

