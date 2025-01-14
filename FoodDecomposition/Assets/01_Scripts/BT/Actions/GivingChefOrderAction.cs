using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using GM.Managers;
using GM.Staffs;
using GM.Data;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GivingChefOrder", story: "[waiter] giving order to chef", category: "Action", id: "2586debe133c54ed939b06473aac8125")]
public partial class GivingChefOrderAction : Action
{
    [SerializeReference] public BlackboardVariable<Waiter> Waiter;

    protected override Status OnStart()
    {
        Waiter.Value.CurrentData.type = OrderType.Cook;
        ManagerHub.Instance.GetManager<StaffManager>().AddOrderData(Waiter.Value.CurrentData);
        return Status.Running;
    }
}

