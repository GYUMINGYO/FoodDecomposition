using GM.Entities;
using GM.Staffs;
using System;
using DG.Tweening;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GiveFood", story: "[waiter] give food with [animationTrigger] and [duration]", category: "Action", id: "aeacfc1ffaf5865bcd0446d18642713b")]
public partial class GiveFoodAction : Action
{
    [SerializeReference] public BlackboardVariable<Waiter> Waiter;
    [SerializeReference] public BlackboardVariable<EntityAnimatorTrigger> AnimationTrigger;
    [SerializeReference] public BlackboardVariable<float> Duration;

    private Transform _tableFoodTrm;
    private bool _isTriggered;
    private Sequence _sequence;
    
    protected override Status OnStart()
    {
        _isTriggered = false;
        AnimationTrigger.Value.OnAnimationEnd += HandleAnimationEnd;
        AnimationTrigger.Value.OnEventAnimationEnd += HandleEventAnimationEnd;
        _tableFoodTrm = Waiter.Value.CurrentData.orderTable.GetFoodPos(Waiter.Value.CurrentData.orderCustomer);
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
        Transform food = Waiter.Value.FoodHandTrm.GetChild(0);
        _sequence = DOTween.Sequence().OnStart(() =>
            {
                food.DOMove(_tableFoodTrm.position, Duration.Value);
                food.parent = _tableFoodTrm;
            })
            .Join(food.DORotateQuaternion(_tableFoodTrm.rotation, Duration.Value));
    }
    
    private void HandleAnimationEnd()
    {
        _isTriggered = true;
    }
}

