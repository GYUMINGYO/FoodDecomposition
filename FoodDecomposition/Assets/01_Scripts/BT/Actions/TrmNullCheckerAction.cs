using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "TrmNullChecker", story: "is [transform] null", category: "Action", id: "b960e0b0d351ab561ed506aaa0d4ec57")]
public partial class TrmNullCheckerAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Transform;

    protected override Status OnUpdate()
    {
        if (Transform.Value != null)
        {
            return Status.Success;
        }
        return Status.Running;
    }
}

