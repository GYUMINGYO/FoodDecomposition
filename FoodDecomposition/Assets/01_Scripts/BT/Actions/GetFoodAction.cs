using GM.Staffs;
using System;
using DG.Tweening;
using GM.Entities;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetFood", story: "[waiter] get food of [foodTrm] with [animationTrigger] and [duration]", category: "Action", id: "ccb2e62da11c583649de765aa614dec9")]
public partial class GetFoodAction : Action
{
    // TODO : 이거 쉐프 음식 두는 거랑 겹친다 수정하셈
    [SerializeReference] public BlackboardVariable<Waiter> Waiter;
    [SerializeReference] public BlackboardVariable<Transform> FoodTrm;
    [SerializeReference] public BlackboardVariable<EntityAnimatorTrigger> AnimationTrigger;
    [SerializeReference] public BlackboardVariable<float> Duration;

    private bool _isTriggered;
    private Sequence _sequence;
    
    protected override Status OnStart()
    {
        _isTriggered = false;
        AnimationTrigger.Value.OnAnimationEnd += HandleAnimationEnd;
        AnimationTrigger.Value.OnEventAnimationEnd += HandleEventAnimationEnd;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return _isTriggered ? Status.Success : Status.Running;
    }

    protected override void OnEnd()
    {
        _sequence.Kill();
        AnimationTrigger.Value.OnAnimationEnd -= HandleAnimationEnd;
        AnimationTrigger.Value.OnEventAnimationEnd -= HandleEventAnimationEnd;
    }

    private void HandleEventAnimationEnd()
    {
        Transform food = FoodTrm.Value.GetChild(0);
        _sequence = DOTween.Sequence().OnStart(() =>
            {
                food.DOMove(Waiter.Value.FoodHandTrm.position, Duration.Value);
                food.parent = Waiter.Value.FoodHandTrm;
            })
            .Join(food.DORotateQuaternion(Waiter.Value.FoodHandTrm.rotation, Duration.Value));
    }
    
    private void HandleAnimationEnd()
    {
        _isTriggered = true;
    }
}

