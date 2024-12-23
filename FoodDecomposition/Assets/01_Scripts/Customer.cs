using GM.Data;
using GM.Entities;

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

    public class Customer : Entity
    {
        public CustomerState CurrentState => _currentState;
        private CustomerState _currentState = CustomerState.FoodComplete;
        private OrderData _orderData;

        public void SuccessOrder() => _currentState = CustomerState.FoodWait;
        //public void SuccessEatFood() => _currentState = CustomerState.CounterWait;

        public void SetOrderData(OrderData orderData) => _orderData = orderData;

        public float GetSellPrice()
        {
            /* if (_orderData == null)
                return _orderData.recipe.sellPrice;
            else
                return 0; */
            return 0;
        }

        //test
        public void ChangeState(CustomerState state)
        {
            _currentState = state;
        }
    }
}