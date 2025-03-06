using GM.Entities;
using GM.Staffs;
using System;
using DG.Tweening;
using GM;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using GM.Managers;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ThrowingAwayFoodTrash", story: "[staff] throwing food of [foodTrashTrm] with [animationTrigger] and [duration]", category: "Action", id: "1660e196c7ffc9e7715880d0c2048684")]
public partial class ThrowingAwayFoodTrashAction : Action
{
    [SerializeReference] public BlackboardVariable<Staff> Staff;
    [SerializeReference] public BlackboardVariable<Transform> FoodTrashTrm;
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
        Transform food = Staff.Value.FoodHandTrm.GetChild(0);
        _sequence = DOTween.Sequence().OnStart(() =>
            {
                food.DOMove(FoodTrashTrm.Value.position, Duration.Value);
            })
            .Join(food.DORotateQuaternion(FoodTrashTrm.Value.rotation, Duration.Value))
            .OnComplete((() =>
            {
                // TODO : 이거 손에서 오브젝트가 없어지게 해야함
                ManagerHub.Instance.Pool.Push(food.GetComponent<Food>());
            }));
    }

    private void HandleAnimationEnd()
    {
        _isTriggered = true;
    }
}

