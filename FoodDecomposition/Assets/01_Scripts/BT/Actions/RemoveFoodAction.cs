using GM;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using GM.Managers;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RemoveFood", story: "remove food of [customer] on the [table]", category: "Action", id: "2f5440e6d103d840a996618064737a12")]
public partial class RemoveFoodAction : Action
{
    [SerializeReference] public BlackboardVariable<Customer> Customer;
    [SerializeReference] public BlackboardVariable<Table> Table;

    protected override Status OnStart()
    {
        this.Table.Value.RemoveFood(Customer.Value);

        //test
        ManagerHub.Instance.GetManager<PreferenceManager>().AddPreferenceGain(Customer.Value, Customer.Value.OrderData.recipe.tasty);

        return Status.Success;
    }
}

