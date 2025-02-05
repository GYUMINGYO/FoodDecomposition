using GM;
using GM.Managers;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RelaseLine", story: "[customer] exits the line", category: "Action", id: "6a6d2e85c5b0313e86d74e16be31d552")]
public partial class ExitLineAction : Action
{
    [SerializeReference] public BlackboardVariable<Customer> Customer;

    protected override Status OnStart()
    {
        SingleCounterEntity counter = ManagerHub.Instance.GetManager<MapManager>().Counter;
        counter.ExitLine(Customer.Value);

        return Status.Success;
    }
}

