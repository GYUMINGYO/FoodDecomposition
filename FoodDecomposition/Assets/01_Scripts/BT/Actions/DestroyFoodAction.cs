using GM;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DestroyFood", story: "destroy food on the [table]", category: "Action", id: "1fd1a38782c5093a2f6225a9ebb0e86d")]
public partial class DestroyFoodAction : Action
{
    [SerializeReference] public BlackboardVariable<Table> Table;

    protected override Status OnStart()
    {
        Table.Value.DestroyFood();
        return Status.Success;
    }
}

