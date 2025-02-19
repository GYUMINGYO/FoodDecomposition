using GM.Staffs;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StaffEndWork", story: "[staff] end work", category: "Action", id: "6d41c9c2fdd797d56006fa2791cb698c")]
public partial class StaffEndWorkAction : Action
{
    [SerializeReference] public BlackboardVariable<Staff> Staff;

    protected override Status OnStart()
    {
        Staff.Value.EndWork();
        return Status.Running;
    }
}

