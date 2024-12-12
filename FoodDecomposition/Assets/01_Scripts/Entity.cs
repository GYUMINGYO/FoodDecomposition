using UnityEngine;
using UnityEngine.AI;

namespace GM.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        public bool CanManualMove => _canManualMove;
        public NavMeshAgent NavAgent => _navAgent;

        private bool _canManualMove = false;

        private NavMeshAgent _navAgent;

        private void Awake()
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
