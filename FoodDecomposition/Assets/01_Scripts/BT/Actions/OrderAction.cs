using GM;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using GM.Data;
using GM.Managers;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Order", story: "[customer] order", category: "Action", id: "738c202e4ec81fbb8f0171d342c291cd")]
public partial class OrderAction : Action
{
    [SerializeReference] public BlackboardVariable<Customer> Customer;

    protected override Status OnStart()
    {
        OrderData order = new OrderData();
        order.orderCustomer = Customer.Value;
        order.recipe = RecipeManager.Instance.GetRecipe();

        Customer.Value.SetOrderData(order);
        ManagerHub.WaiterManager.AddOrderData(order);
        Debug.Log("order");

        return Status.Success;
    }
}