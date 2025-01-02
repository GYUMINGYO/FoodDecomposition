using GM;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CreateFood", story: "create food the [customer] is on [chair] the [table]", category: "Action", id: "4f58a7766d42eada1c2420c6ad6ea260")]
public partial class CreateFoodAction : Action
{
    [SerializeReference] public BlackboardVariable<Customer> Customer;
    [SerializeReference] public BlackboardVariable<Transform> Chair;
    [SerializeReference] public BlackboardVariable<Table> Table;
    protected override Status OnStart()
    {
        PoolTypeSO poolType = Customer.Value.GetOrderData().recipe.poolType;
        Table.Value.CreateFood(Chair.Value, poolType);

        return Status.Success;
    }
}

