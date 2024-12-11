using UnityEngine;
using UnityEngine.AI;

namespace GM
{
    public enum CustomerState
    {
        OrderWait,
        FoodWait,
        FoodComplete,
        CounterWait
    }

    public class Customer : MonoBehaviour
    {
        public NavMeshAgent Agent => _agent;

        private NavMeshAgent _agent;

        public CustomerState CurrentState => _currentState;
        private CustomerState _currentState = CustomerState.OrderWait;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public void MoveToTarget(Transform targetTrm)
        {
            _agent.SetDestination(targetTrm.position);
        }

        public void SuccessOrder() => _currentState = CustomerState.FoodWait;
        //public void SuccessEatFood() => _currentState = CustomerState.CounterWait;
    }
}