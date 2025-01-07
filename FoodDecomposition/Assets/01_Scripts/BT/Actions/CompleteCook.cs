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
[NodeDescription(name: "CompleteCook", story: "[chef] complete cook to food put [foodTrm] with [animationTrigger]", category: "Action", id: "1751848048f6d29886b059c0f220936a")]
public partial class CompleteCookAction : Action
{
    [SerializeReference] public BlackboardVariable<Chef> Chef;
    [SerializeReference] public BlackboardVariable<Transform> FoodTrm;
    [SerializeReference] public BlackboardVariable<EntityAnimatorTrigger> AnimationTrigger;
    private bool _isTriggered;
    
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
        AnimationTrigger.Value.OnEventAnimationEnd -= HandleEventAnimationEnd;
    }

    private void HandleEventAnimationEnd()
    {
        Transform food = Chef.Value.FoodTrm.GetChild(0);
        food.DOMove(FoodTrm.Value.position, 0.25f).OnComplete(() =>
        {
            OrderData data = Chef.Value.CurrentData;
            data.type = OrderType.Serving;
            ManagerHub.Instance.GetManager<WaiterManager>().AddOrderData(data);
            _isTriggered = true;
        });
        food.rotation = Quaternion.identity;
        food.parent = FoodTrm.Value;
    }
}

