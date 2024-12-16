using GM;
using DG.Tweening;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SitChair", story: "[customer] sits on a [chair]", category: "Action", id: "8fea6602a0afc066868a92075dabf51d")]
public partial class SitChairAction : Action
{
    [SerializeReference] public BlackboardVariable<Customer> Customer;
    [SerializeReference] public BlackboardVariable<Transform> Chair;

    private float _jumpPower = 0.7f;

    protected override Status OnStart()
    {
        Vector3 lookDir = Chair.Value.forward;
        Status status = Status.Running;

        Sequence seq = DOTween.Sequence()
        .OnStart(() =>
        {
            Customer.Value.transform.DOJump(Chair.Value.position, _jumpPower, 1, 0.5f);
        })
        .Append(Customer.Value.transform.DORotate(lookDir, 0.5f))
        .OnComplete(() =>
        {
            status = Status.Success;
        });
        return status;
    }
}