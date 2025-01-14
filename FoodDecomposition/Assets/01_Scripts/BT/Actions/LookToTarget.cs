using DG.Tweening;
using System;
using GM.Entities;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "LookToTarget", story: "[unit] looks at [target] for [duration] with [lookAt]", category: "Action", id: "bc23f46567b4bd64814a625ba0cb6bc7")]
public partial class LookTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Unit> Unit;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<float> Duration;
    [SerializeReference] public BlackboardVariable<bool> LookAt;

    private Status status = Status.Running;

    protected override Status OnStart()
    {
        status = Status.Running;

        Unit.Value.NavAgent.velocity = Vector3.zero;
        Quaternion targetQuaternion;

        if (LookAt == true)
        {
            Vector3 direction = Target.Value.position - Unit.Value.transform.position;
            direction.y = 0;
            targetQuaternion = Quaternion.LookRotation(direction);
        }
        else
        {
            targetQuaternion = Target.Value.rotation;
        }

        Unit.Value.transform.DORotateQuaternion(targetQuaternion, Duration.Value).SetEase(Ease.Linear)
            .OnComplete(() => status = Status.Success);

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return status;
    }
}