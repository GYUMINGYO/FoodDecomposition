using GM;
using GM._01_Scripts.Data;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using GM.Manager;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RequstForCount", story: "[customer] count", category: "Action", id: "7575c5fe3b568204342da7437584d46c")]
public partial class RequstForCountAction : Action
{
    [SerializeReference] public BlackboardVariable<Customer> Customer;

    protected override Status OnStart()
    {
        WaiterManager.Instance.AddCounterList(Customer.Value);
        return Status.Success;
    }
}

