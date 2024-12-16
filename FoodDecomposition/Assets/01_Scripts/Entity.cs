using UnityEngine;
using UnityEngine.AI;

namespace GM.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        public NavMeshAgent NavAgent => _navAgent;
        public bool CanManualMove => _canManualMove;

        protected NavMeshAgent _navAgent;
        protected bool _canManualMove = false;

        protected virtual void Awake()
        {
            _navAgent = GetComponent<NavMeshAgent>();
        }

        /// <summary>
        /// Set Movement Destination
        /// </summary>
        /// <param name="targetTrm">target Transform to move</param>
        public virtual void SetMovement(Transform targetTrm)
        {
            _navAgent.SetDestination(targetTrm.position);
        }

        /// <summary>
        /// Move Stop
        /// </summary>
        public void StopImmediately()
        {
            _navAgent.isStopped = true;
        }
    }
}
