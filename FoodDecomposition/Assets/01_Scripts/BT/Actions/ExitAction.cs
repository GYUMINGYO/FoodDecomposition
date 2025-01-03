using GM;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Exit", story: "[customer] exit", category: "Action", id: "c96c6e3bd25b33bdccac14094542029d")]
public partial class ExitAction : Action
{
    [SerializeReference] public BlackboardVariable<Customer> Customer;

    protected override Status OnStart()
    {
        SingletonePoolManager.Instance.Push(Customer.Value);
        MapManager.Instance.ReleaseCount();

        return Status.Success;
    }
}

