using GM;
using GM.InteractableEntitys;
using GM.Managers;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsFrontEmpty", story: "[customer] checks if [counter] ahead is empty", category: "Conditions", id: "819d381d8c26f5556a845075c1bd39e0")]
public partial class IsFrontEmptyCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Customer> Customer;
    [SerializeReference] public BlackboardVariable<Transform> Counter;

    public override bool IsTrue()
    {
        InteractableEntity entity = null;
        if (ManagerHub.Instance.GetManager<RestourantManager>().GetInteractableEntity(Enums.InteractableEntityType.Counter, out entity, Customer.Value))
        {
            SingleCounterEntity counter = entity as SingleCounterEntity;

            Transform counterTrm = counter.CheckEmptyFront(Customer.Value);

            if(counterTrm != null)
            {
                Counter.Value = counterTrm;
                return true;
            }
        }
        return false;
    }

    public override void OnStart()
    {
        Customer.Value.SetIsLine(true);
    }
}
