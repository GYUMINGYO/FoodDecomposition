using GM;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using GM.Data;
using GM.Managers;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Order", story: "[Customer] places order at the [table]", category: "Action", id: "738c202e4ec81fbb8f0171d342c291cd")]
public partial class OrderAction : Action
{
    [SerializeReference] public BlackboardVariable<Customer> Customer;
    [SerializeReference] public BlackboardVariable<Table> Table;

    protected override Status OnStart()
    {
        OrderData order = new OrderData();
        order.orderTable = Table.Value;
        order.orderCustomer = Customer.Value;
        order.type = OrderType.Order;
        order.recipe = ManagerHub.Instance.GetManager<RecipeManager>().GetRecipe();

        Customer.Value.SetOrderData(order);
        ManagerHub.Instance.GetManager<StaffManager>().AddOrderData(order);

        return Status.Success;
    }
}