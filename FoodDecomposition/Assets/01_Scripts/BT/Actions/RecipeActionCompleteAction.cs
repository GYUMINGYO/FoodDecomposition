using GM.Staffs;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RecipeActionComplete", story: "[chef] to recipe action complete", category: "Action", id: "ad3f7ae12eab070f040d6d6f974b108a")]
public partial class RecipeActionCompleteAction : Action
{
    [SerializeReference] public BlackboardVariable<Chef> Chef;

    protected override Status OnStart()
    {
        Chef.Value.CurrentData.recipe.NextCookingTable();
        return Status.Success;
    }
}

