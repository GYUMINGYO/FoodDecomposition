using GM.Staffs;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StaffEndChange", story: "[staff] end change", category: "Action", id: "5a4257b7a22ce0352ef3a8f2eabefe25")]
public partial class StaffEndChangeAction : Action
{
    [SerializeReference] public BlackboardVariable<Staff> Staff;

    protected override Status OnStart()
    {
        Staff.Value.StaffEndChnage();
        return Status.Running;
    }
}

