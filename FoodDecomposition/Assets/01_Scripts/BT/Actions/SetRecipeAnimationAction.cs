using GM.Staffs;
using System;
using GM.CookWare;
using GM.Entities;
using GM.InteractableEntitys;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetRecipeAnimationAction", story: "set current recipe animation of [chef] with [entityAnimator] and [cookingSpeed]", category: "Action", id: "613fc65ead7fe9a85ab296ae6303362f")]
public partial class SetRecipeAnimationActionAction : Action
{
    [SerializeReference] public BlackboardVariable<Chef> Chef;
    [SerializeReference] public BlackboardVariable<EntityAnimator> EntityAnimator;
    [SerializeReference] public BlackboardVariable<float> CookingSpeed;
    protected override Status OnStart()
    {
        // TODO : 일단 if문을 때우고 나중에 구조 잡기

        CookingTable cookingTable = Chef.Value.CurrentData.recipe.GetCurrentCookingTable();
        if (cookingTable is Refrigerator refrigerator)
        {
            refrigerator.SetChef(Chef.Value);
        }

        EntityAnimator.Value.SetCookingAnimation(cookingTable.CookAnimation, CookingSpeed.Value);
        return Status.Running;
    }
}

