using DG.Tweening;
using GM;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SitChair", story: "[self] sits on a [chair]", category: "Action", id: "8fea6602a0afc066868a92075dabf51d")]
public partial class SitChairAction : Action
{
    [SerializeReference] public BlackboardVariable<Customer> Self;
    [SerializeReference] public BlackboardVariable<Transform> Chair;

    private float _jumpPower = 0.7f;
    private Status _status = Status.Running;

    protected override Status OnStart()
    {
        Chair.Value.GetComponent<NavMeshObstacle>().enabled = false;

        Vector3 movePos = Chair.Value.transform.position;
        movePos.y = Self.Value.transform.position.y;
        Vector3 lookDir = Chair.Value.transform.localRotation * Vector3.forward;

        Sequence seq = DOTween.Sequence()
        .OnStart(() =>
        {
            Self.Value.transform.DOJump(movePos, _jumpPower, 1, 0.5f);
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