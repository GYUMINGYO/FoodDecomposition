using DG.Tweening;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "LookToTarget", story: "[self] looks at [target] for [duration] with [lookAt]", category: "Action", id: "bc23f46567b4bd64814a625ba0cb6bc7")]
public partial class LookTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<float> Duration;
    [SerializeReference] public BlackboardVariable<bool> LookAt;
    
    private Status status = Status.Running;

    protected override Status OnStart()
    {
        status = Status.Running;

        Self.Value.GetComponent<NavMeshAgent>().velocity = Vector3.zero;

        Quaternion targetQuaternion;
        if (LookAt == true)
        {
            Vector3 dir = Target.Value.position - Self.Value.transform.position;
            targetQuaternion = Quaternion.Euler(dir);
        }
        else
        {
            targetQuaternion = Target.Value.rotation;
        }

        Self.Value.transform.DORotateQuaternion(targetQuaternion, Duration.Value).SetEase(Ease.Linear)
            .OnComplete(() => status = Status.Success);

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return status;
    }
}