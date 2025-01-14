using GM.Entities;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToTarget", story: "[unit] move to [target]", category: "Action", id: "d0d3edfff31fee69d64e268c987e512f")]
public partial class MoveToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Unit> Unit;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<Animator> MainAnimator;
    [SerializeReference] public BlackboardVariable<float> Speed = new BlackboardVariable<float>(1.0f);
    [SerializeReference] public BlackboardVariable<float> DistanceThreshold = new BlackboardVariable<float>(0.2f);
    [SerializeReference] public BlackboardVariable<string> AnimatorSpeedParam = new BlackboardVariable<string>("Velocity");
    [SerializeReference] public BlackboardVariable<float> SlowDownDistance = new BlackboardVariable<float>(1.0f);

    private NavMeshAgent _navMeshAgent;
    private Vector3 m_LastTargetPosition;

    protected override Status OnStart()
    {
        _navMeshAgent = Unit.Value.NavAgent;

        if (_navMeshAgent == null)
        {
            return Status.Failure;
        }

        return Initialize();
    }

    protected override Status OnUpdate()
    {
        if (Unit.Value == null || Target.Value == null)
        {
            return Status.Failure;
        }

        // Check if the target position has changed.
        bool boolUpdateTargetPosition = !Mathf.Approximately(m_LastTargetPosition.x, Target.Value.transform.position.x) || !Mathf.Approximately(m_LastTargetPosition.y, Target.Value.transform.position.y) || !Mathf.Approximately(m_LastTargetPosition.z, Target.Value.transform.position.z);
        if (boolUpdateTargetPosition)
        {
            m_LastTargetPosition = Target.Value.transform.position;
        }

        float distance = GetDistanceXZ();

        if (distance <= DistanceThreshold)
        {
            return Status.Success;
        }

        if (_navMeshAgent != null)
        {
            if (boolUpdateTargetPosition)
            {
                _navMeshAgent.SetDestination(Target.Value.position);
            }
        }
        else
        {
            float speed = Speed;

            if (SlowDownDistance > 0.0f && distance < SlowDownDistance)
            {
                float ratio = distance / SlowDownDistance;
                speed = Mathf.Max(0.1f, Speed * ratio);
            }

            Vector3 agentPosition = Unit.Value.transform.position;
            Vector3 toDestination = Target.Value.position - agentPosition;
            toDestination.y = 0.0f;
            toDestination.Normalize();
            agentPosition += toDestination * (speed * Time.deltaTime);
            Unit.Value.transform.position = agentPosition;

            // Look at the target.
            Unit.Value.transform.forward = toDestination;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
        if (MainAnimator.Value != null)
        {
            MainAnimator.Value.SetFloat(AnimatorSpeedParam, 0);
        }

        if (_navMeshAgent != null)
        {
            if (_navMeshAgent.isOnNavMesh)
            {
                _navMeshAgent.ResetPath();
            }
        }

        _navMeshAgent = null;
    }

    protected override void OnDeserialize()
    {
        Initialize();
    }

    private Status Initialize()
    {
        m_LastTargetPosition = Target.Value.transform.position;

        if (GetDistanceXZ() <= DistanceThreshold)
        {
            return Status.Success;
        }

        // If using animator, set speed parameter.
        if (MainAnimator.Value != null)
        {
            MainAnimator.Value.SetFloat(AnimatorSpeedParam, Speed);
        }

        // If using a navigation mesh, set target position for navigation mesh agent.
        if (_navMeshAgent != null)
        {
            if (_navMeshAgent.isOnNavMesh)
            {
                _navMeshAgent.ResetPath();
            }
            _navMeshAgent.speed = Speed;

            _navMeshAgent.stoppingDistance = DistanceThreshold;
            _navMeshAgent.SetDestination(Target.Value.position);
        }

        return Status.Running;
    }

    private float GetDistanceXZ()
    {
        return Vector3.Distance(Unit.Value.transform.position, Target.Value.position);
    }
}