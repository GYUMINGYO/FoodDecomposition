using GM;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CustomerWait", story: "[customer] waiting", category: "Action", id: "674492e3d1d1089335f651bd3b688f2c")]
public partial class CustomerWaitAction : Action
{
    [SerializeReference] public BlackboardVariable<Customer> Customer;
    protected override Status OnStart()
    {
        Customer.Value.SetWait(true);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Customer.Value.IsWait ? Status.Running : Status.Success;
    }
}

