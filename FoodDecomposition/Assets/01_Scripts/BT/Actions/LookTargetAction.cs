using DG.Tweening;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "LookTarget", story: "[self] looks at [target] for [duration]", category: "Action", id: "bc23f46567b4bd64814a625ba0cb6bc7")]
public partial class LookTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<float> Duration;

    Status status = Status.Running;

    protected override Status OnStart()
    {
        Self.Value.GetComponent<NavMeshAgent>().velocity = Vector3.zero;

        Self.Value.transform.DORotateQuaternion(Target.Value.rotation, Duration.Value)
            .OnComplete(() => status = Status.Success);

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return status;
    }
}