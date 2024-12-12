using GM;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CustomerSuccessOrder", story: "[customer] order [state]", category: "Conditions", id: "0110cb397b352888d764cfc59ccd8d31")]
public partial class CustomerSuccessOrderCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Customer> Customer;
    [SerializeReference] public BlackboardVariable<CustomerState> State;

    public override bool IsTrue()
    {
        if (Customer.Value.CurrentState == State.Value)
        {
            return true;
        }

        return false;
    }
}
