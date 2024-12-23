using GM.Staffs;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FinishWork", story: "[staff] finished work", category: "Action", id: "b910f7b19d31b7acb0281d49651b7f39")]
public partial class FinishWorkAction : Action
{
    [SerializeReference] public BlackboardVariable<Staff> Staff;

    protected override Status OnStart()
    {
        Staff.Value.FinishWork();
        return Status.Success;
    }
}

