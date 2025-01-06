using GM.Staffs;
using System;
using GM.Entities;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetRecipeAnimationAction", story: "set current recipe animation of [chef] with [entityAnimator]", category: "Action", id: "613fc65ead7fe9a85ab296ae6303362f")]
public partial class SetRecipeAnimationActionAction : Action
{
    [SerializeReference] public BlackboardVariable<Chef> Chef;
    [SerializeReference] public BlackboardVariable<EntityAnimator> EntityAnimator;

    protected override Status OnStart()
    {
        EntityAnimator.Value.SetCookingAnimation(Chef.Value.CurrentData.recipe.GetCurrentCookingTableAnimation());
        return Status.Running;
    }
}

