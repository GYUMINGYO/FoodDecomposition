using GM;
using GM.Data;
using GM.InteractableEntitys;
using GM.Managers;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RequstForCount", story: "[customer] count", category: "Action", id: "7575c5fe3b568204342da7437584d46c")]
public partial class RequstForCountAction : Action
{
    [SerializeReference] public BlackboardVariable<Customer> Customer;

    protected override Status OnStart()
    {
        this.Customer.Value.SetIsLine(false);

        OrderData order = Customer.Value.GetOrderData();
        order.type = OrderType.Count;
        ManagerHub.Instance.GetManager<StaffManager>().AddOrderData(order);
        return Status.Success;
    }
}