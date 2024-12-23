using GM;
using DG.Tweening;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SitChair", story: "[customer] sits on a [chair]", category: "Action", id: "8fea6602a0afc066868a92075dabf51d")]
public partial class SitChairAction : Action
{
    [SerializeReference] public BlackboardVariable<Customer> Customer;
    [SerializeReference] public BlackboardVariable<Transform> Chair;

    private float _jumpPower = 0.7f;
    private Status _status = Status.Running;

    protected override Status OnStart()
    {
        Vector3 movePos = Chair.Value.transform.position;
        movePos.y = Customer.Value.transform.position.y;
        Vector3 lookDir = Chair.Value.transform.localRotation * Vector3.forward;

        Sequence seq = DOTween.Sequence()
        .OnStart(() =>
        {
            Customer.Value.transform.DOJump(movePos, _jumpPower, 1, 0.5f);
        })
        .Append(Customer.Value.transform.DORotate(lookDir, 0.5f))
        .OnComplete(() =>
        {
            _status = Status.Success;
        });

        return _status;
    }

    protected override Status OnUpdate()
    {
        return _status;
    }
}