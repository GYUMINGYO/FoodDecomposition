using GM.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WaitForAnimation", story: "wait for [animationTrigger]", category: "Action", id: "457b19e3d08c897ffe859fce85a3fcb9")]
public partial class WaitForAnimationAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityAnimatorTrigger> AnimationTrigger;

    private bool _isTriggered;
    
    protected override Status OnStart()
    {
        _isTriggered = false;
        AnimationTrigger.Value.OnAnimationEnd += HandleAnimationEnd;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return _isTriggered ? Status.Success : Status.Running;
    }

    protected override void OnEnd()
    {
        AnimationTrigger.Value.OnAnimationEnd -= HandleAnimationEnd;
    }

    private void HandleAnimationEnd()
    {
        _isTriggered = true;
    }
}