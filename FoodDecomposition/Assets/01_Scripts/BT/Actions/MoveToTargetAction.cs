using GM;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToTarget", story: "[customer] move to [target]", category: "Action", id: "d0d3edfff31fee69d64e268c987e512f")]
public partial class MoveToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Customer> Customer;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    private NavMeshAgent _navMeshAgent;

    protected override Status OnStart()
    {
        Customer.Value.MoveToTarget(Target.Value);
        _navMeshAgent = Customer.Value.Agent;

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