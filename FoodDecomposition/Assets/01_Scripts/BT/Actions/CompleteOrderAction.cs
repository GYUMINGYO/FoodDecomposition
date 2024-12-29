using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using GM;
using GM.Staffs;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CompleteOrder", story: "[staff] Is [complete] the order", category: "Action", id: "3f97e87114de836287a79336ae9e2e0a")]
public partial class CompleteOrderAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> Complete;
    [SerializeReference] public BlackboardVariable<Staff> Staff;

    protected override Status OnStart()
    {
        Staff.Value.CurrentData.orderCustomer.SetWait(Complete.Value);
        return Status.Success;
    }
}

