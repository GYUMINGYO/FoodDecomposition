using GM.Staffs;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsOutCustomer", story: "[waiter] check customer out", category: "Conditions", id: "b7f247c29be4e63f4e20286692838c24")]
public partial class IsOutCustomerCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Waiter> Waiter;

    public override bool IsTrue()
    {
        if (Waiter.Value.CurrentData.isCustomerOut == true)
        {
            return true;
        }
        
        return false;
    }
}
