using GM;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using GM.Managers;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RequstForCount", story: "[customer] count", category: "Action", id: "7575c5fe3b568204342da7437584d46c")]
public partial class RequstForCountAction : Action
{
    [SerializeReference] public BlackboardVariable<Customer> Customer;

    protected override Status OnStart()
    {
        ManagerHub.WaiterManager.AddListData(Customer.Value);
        return Status.Success;
    }
}

