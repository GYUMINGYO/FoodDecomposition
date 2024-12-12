using GM;
using GM.Manager;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "FindTable", story: "[customer] find [chiar]", category: "Conditions", id: "57b0d6a1ee99ebcc7a6bff878ade6f75")]
public partial class FindTableCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Customer> Customer;
    [SerializeReference] public BlackboardVariable<Transform> Chiar;

    public override bool IsTrue()
    {
        Chiar.Value = RestourantManager.Instance.GetChiar();

        if (Chiar.Value == null)
        {
            return false;
        }

        return true;
    }
}
