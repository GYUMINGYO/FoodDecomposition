using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using GM;

[BlackboardEnum]
public enum MoveTargetType
{
    Counter,
    Exit
}

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetMoveTarget", story: "get [target] for [type]", category: "Action", id: "34eef2556090225664411bdf4cea9ee3")]
public partial class GetMoveTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<MoveTargetType> Type;

    protected override Status OnStart()
    {
        switch (this.Type.Value)
        {
            case MoveTargetType.Counter:
                Target.Value = MapManager.Instance.CounterTrm;
                break;
            case MoveTargetType.Exit:
                Target.Value = MapManager.Instance.ExtrenceAndExitTrm;
                break;
        }

        return Status.Success;
    }
}