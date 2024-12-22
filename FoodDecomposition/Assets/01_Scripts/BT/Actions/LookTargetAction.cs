using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using GM;
using DG.Tweening;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "LookTarget", story: "[customer] look at [target]", category: "Action", id: "0c59cf14010832474b2de51dcf27c342")]
public partial class LookTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Customer> Customer;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    Status status = Status.Running;

    protected override Status OnStart()
    {
        Customer.Value.transform.DORotate(Target.Value.localRotation * Vector3.forward, 0.1f)
            .OnComplete(() =>
            {
                status = Status.Success;
            });

        return status;
    }

    protected override Status OnUpdate()
    {
        return status;
    }
}

