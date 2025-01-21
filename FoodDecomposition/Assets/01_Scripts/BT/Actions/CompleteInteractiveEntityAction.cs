using GM.Staffs;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CompleteInteractiveEntity", story: "[staff] to interactiveEntity Use end", category: "Action", id: "eb7a11e830c68276bc1b8a992f915348")]
public partial class CompleteInteractiveEntityAction : Action
{
    [SerializeReference] public BlackboardVariable<Staff> Staff;

    protected override Status OnStart()
    {
        Staff.Value.EndTable();
        return Status.Success;
    }
}

