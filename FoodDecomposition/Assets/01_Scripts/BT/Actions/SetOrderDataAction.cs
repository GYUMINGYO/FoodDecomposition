using GM.Data;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetOrderData", story: "set [orderData]", category: "Action", id: "952774f4bc6daed5e3c3e69aa0755b69")]
public partial class SetOrderDataAction : Action
{
    [SerializeReference] public BlackboardVariable<OrderData> OrderData;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

