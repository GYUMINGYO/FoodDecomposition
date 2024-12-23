using GM.Entities;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToTarget", story: "[entity] move to [target]", category: "Action", id: "d0d3edfff31fee69d64e268c987e512f")]
public partial class MoveToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Entity> Entity;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    private NavMeshAgent _navMeshAgent;

    protected override Status OnStart()
    {
        Entity.Value.SetMovement(Target.Value);
        _navMeshAgent = Entity.Value.NavAgent;

        if (_navMeshAgent == null)
        {
            Debug.LogError("not agent");
            return Status.Failure;
        }
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            return Status.Success;
        }
        return Status.Running;
    }
}