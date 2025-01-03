using GM;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using GM.Managers;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StandChair", story: "stand up from the [chair] at the [table]", category: "Action", id: "595516d524148b7ca3e87b1080d95aa0")]
public partial class StandChairAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Chair;
    [SerializeReference] public BlackboardVariable<Table> Table;

    protected override Status OnStart()
    {
        ManagerHub.Instance.GetManager<RestourantManager>().chairDictionary[Chair.Value] = false;
        Table.Value.SetObstacle(Chair.Value, true);

        return Status.Success;
    }
}

