using GM.Staffs;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StaffChange", story: "[staff] change", category: "Action", id: "15d3d058bdc69b490bb10c6e4ce83647")]
public partial class StaffChangeAction : Action
{
    [SerializeReference] public BlackboardVariable<Staff> Staff;

    protected override Status OnStart()
    {
        Staff.Value.StaffChangeEvent();
        return Status.Success;
    }
}

