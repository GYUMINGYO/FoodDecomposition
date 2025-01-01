using GM.Staffs;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using GM;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetRecipeTarget", story: "[chef] set recipe [cookingPath]", category: "Action", id: "568b868c2c734ea7097b4136d3b61304")]
public partial class SetRecipeTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Chef> Chef;
    [SerializeReference] public BlackboardVariable<Transform> CookingPath;

    protected override Status OnStart()
    {
        CookingTable cookingTable = Chef.Value.CurrentData.recipe.GetNextCookingTable();
        if (cookingTable == null)
        {
            return Status.Failure;
        }

        CookingPath.Value = cookingTable.transform;

        return Status.Success;
    }
}

