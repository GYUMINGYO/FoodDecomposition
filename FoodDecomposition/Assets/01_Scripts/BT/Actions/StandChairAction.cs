using GM;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StandChair", story: "stand up from the [chair] at the [table]", category: "Action", id: "595516d524148b7ca3e87b1080d95aa0")]
public partial class StandChairAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Chair;
    [SerializeReference] public BlackboardVariable<Table> Table;

    protected override Status OnStart()
    {
        Table.Value.StandChair(Chair.Value);

        return Status.Success;
    }
}

