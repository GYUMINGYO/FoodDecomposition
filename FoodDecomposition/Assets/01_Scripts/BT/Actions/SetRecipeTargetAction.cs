using GM.Staffs;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetRecipeTarget", story: "[chef] set recipe [cookingPath]", category: "Action", id: "568b868c2c734ea7097b4136d3b61304")]
public partial class SetRecipeTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Chef> Chef;
    [SerializeReference] public BlackboardVariable<Transform> CookingPath;

    protected override Status OnStart()
    {
        //CookingPath.Value = Chef.Value.CurrentData.recipe.cookingPath.NextPath();
        return Status.Success;
    }
}

