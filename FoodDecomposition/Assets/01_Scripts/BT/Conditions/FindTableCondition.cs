using GM;
using GM.Managers;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "FindTable", story: "[customer] find [chair]", category: "Conditions", id: "57b0d6a1ee99ebcc7a6bff878ade6f75")]
public partial class FindTableCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Customer> Customer;
    [SerializeReference] public BlackboardVariable<Transform> Chair;

    public override bool IsTrue()
    {
        Chair.Value = ManagerHub.Instance.GetManager<RestourantManager>().GetChiar();

        if (Chair.Value == null)
        {
            return true;
        }

        return false;
    }
}
