using GM.Staffs;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "OrderComplete", story: "[staff] to complete order", category: "Action", id: "6cabe9589e9aca8cac89cd7a610ea38b")]
public partial class OrderCompleteAction : Action
{
    [SerializeReference] public BlackboardVariable<Staff> Staff;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }
}

