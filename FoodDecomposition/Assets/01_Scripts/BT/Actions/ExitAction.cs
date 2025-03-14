using GM;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using GM.Managers;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Exit", story: "[customer] exit", category: "Action", id: "c96c6e3bd25b33bdccac14094542029d")]
public partial class ExitAction : Action
{
    [SerializeReference] public BlackboardVariable<Customer> Customer;

    protected override Status OnStart()
    {
        ManagerHub.Instance.Pool.Push(Customer.Value);
        ManagerHub.Instance.GetManager<DataManager>().SubtractCustomerCnt();
        ManagerHub.Instance.GetManager<PreferenceManager>().ApplyFinalPreference(Customer.Value);

        return Status.Success;
    }
}

