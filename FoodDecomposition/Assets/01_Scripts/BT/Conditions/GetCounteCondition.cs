using GM;
using GM.CookWare;
using GM.Managers;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "GetMoveTarget", story: "Get [customer] [counter] queue", category: "Conditions", id: "0d5316b5fcbe73dd4830370e6a551abc")]
public partial class GetCounterCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Customer> Customer;
    [SerializeReference] public BlackboardVariable<Transform> Counter;

    public override bool IsTrue()
    {
        InteractableEntity entity = null;

        if (ManagerHub.Instance.GetManager<RestourantManager>().GetInteractableEntity(Enums.InteractableEntityType.Counter, out entity, Customer.Value))
        {
            SingleCounterEntity counter = entity as SingleCounterEntity;

            Counter.Value = counter.GetLineTrm(Customer.Value);
            return !(Counter.Value == null);
        }
        else
        {
            return false;
        }
    }
}
