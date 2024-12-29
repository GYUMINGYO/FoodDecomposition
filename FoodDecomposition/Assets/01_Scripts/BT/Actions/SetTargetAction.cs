using GM.Staffs;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetTarget", story: "[staff] set to Taarget", category: "Action", id: "1281637283cb66ce82308e635cc1e1e2")]
public partial class SetTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Staff> Staff;

    protected override Status OnStart()
    {
        // TODO : staff가 가질 수 있게 범용적으로 처리

        Waiter waiter = Staff.Value as Waiter;
        waiter.SetTarget();

        return Status.Success;
    }
}

