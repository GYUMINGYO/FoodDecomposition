using DG.Tweening;
using GM;
using GM.Managers;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SitChair", story: "[customer] sits on a [table] fornt [chair]", category: "Action", id: "8fea6602a0afc066868a92075dabf51d")]
public partial class SitChairAction : Action
{
    [SerializeReference] public BlackboardVariable<Customer> Customer;
    [SerializeReference] public BlackboardVariable<Table> Table;
    [SerializeReference] public BlackboardVariable<Transform> Chair;

    private float _jumpPower = 0.7f;
    private Status _status = Status.Running;

    protected override Status OnStart()
    {
        Table.Value.ChangeChairState(Chair.Value, true, Customer.Value);

        Vector3 movePos = Chair.Value.transform.position;
        movePos.y = Customer.Value.transform.position.y;

        Sequence seq = DOTween.Sequence()
        .OnStart(() =>
        {
            Customer.Value.transform.DOJump(movePos, _jumpPower, 1, 0.5f);
        })
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