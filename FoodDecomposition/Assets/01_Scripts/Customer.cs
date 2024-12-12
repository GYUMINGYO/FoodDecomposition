using GM._01_Scripts.Data;
using Unity.VisualScripting;
using UnityEditor.Build.Pipeline;
using UnityEngine;
using UnityEngine.AI;

namespace GM
{
    public enum CustomerState
    {
        OrderWait,
        FoodWait,
        FoodComplete,
        CounterWait,
        CounterComplete
    }

    public class Customer : MonoBehaviour
    {
        public NavMeshAgent Agent => _agent;

        private NavMeshAgent _agent;

        public CustomerState CurrentState => _currentState;
        private CustomerState _currentState = CustomerState.OrderWait;

        private OrderData _orderData;

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

        public void SetOrderData(OrderData orderData) => _orderData = orderData;

        public float GetSellPrice()
        {
            if (_orderData)
                return _orderData.recipe.sellPrice;
            else
                return 0;
        }

        //test
        public void ChangeState(CustomerState state)
        {
            _currentState = state;
        }
    }
}