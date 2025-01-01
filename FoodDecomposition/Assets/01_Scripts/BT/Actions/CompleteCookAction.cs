using GM.Staffs;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using GM.Managers;
using GM.Data;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CompleteCook", story: "[chef] complete cook to food", category: "Action", id: "1751848048f6d29886b059c0f220936a")]
public partial class CompleteCookAction : Action
{
    [SerializeReference] public BlackboardVariable<Chef> Chef;

    protected override Status OnStart()
    {
        OrderData data = Chef.Value.CurrentData;
        data.type = OrderType.Serving;
        ManagerHub.Instance.GetManager<WaiterManager>().AddOrderData(data);
        return Status.Running;
    }
}

