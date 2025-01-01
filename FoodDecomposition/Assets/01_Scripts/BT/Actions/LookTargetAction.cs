using DG.Tweening;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "LookTarget", story: "[self] looks at [target] for [speed]", category: "Action", id: "bc23f46567b4bd64814a625ba0cb6bc7")]
public partial class LookTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<float> Speed;

    protected override Status OnStart()
    {
        Self.Value.transform.DORotate(Target.Value.forward, 0.5f);
        return Status.Success;
    }
}