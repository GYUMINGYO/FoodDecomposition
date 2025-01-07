using GM.Staffs;
using System;
using DG.Tweening;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using GM.Managers;
using GM.Data;
using GM.Entities;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CompleteCook", story: "[chef] complete cook to food put [foodTrm] with [animationTrigger] and [duration]", category: "Action", id: "1751848048f6d29886b059c0f220936a")]
public partial class CompleteCookAction : Action
{
    // TODO : Waiter랑 음식 가져가는 거랑 겹치니까 음식 주문 부분을 액션으로 빼고 통일성 있게 하면 될듯
    [SerializeReference] public BlackboardVariable<Chef> Chef;
    [SerializeReference] public BlackboardVariable<Transform> FoodTrm;
    [SerializeReference] public BlackboardVariable<EntityAnimatorTrigger> AnimationTrigger;
    [SerializeReference] public BlackboardVariable<float> Duration;
    
    private bool _isTriggered;
    private Sequence _sequence;
    
    protected override Status OnStart()
    {
        _isTriggered = false;
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
        AnimationTrigger.Value.OnEventAnimationEnd -= HandleEventAnimationEnd;
    }

    private void HandleEventAnimationEnd()
    {
        Transform food = Chef.Value.FoodHandTrm.GetChild(0);
        _sequence = DOTween.Sequence().OnStart(() =>
        {
            food.DOMove(FoodTrm.Value.position, Duration.Value);
            food.parent = FoodTrm.Value;
        })
        .Join(food.DORotateQuaternion(Quaternion.identity, Duration.Value))
        .OnComplete(() =>
        {
            OrderData data = Chef.Value.CurrentData;
            data.type = OrderType.Serving;
            ManagerHub.Instance.GetManager<WaiterManager>().AddOrderData(data);
            _isTriggered = true;
        });
    }
}

