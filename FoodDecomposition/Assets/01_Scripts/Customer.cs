using GM.Data;
using GM.Entities;
using Unity.Behavior;

namespace GM
{

    [BlackboardEnum]
    public enum CustomerState
    {
        Order,
        Food,
        Counter
    }

    public class Customer : Entity
    {
        private OrderData _orderData;

        private bool isWait = false;
        public bool IsWait => isWait;

        public void SetOrderData(OrderData orderData) => _orderData = orderData;

        public float GetSellPrice()
        {
            if (_orderData)
                return _orderData.recipe.sellPrice;
            else
                return 0;
        }

        public void SetWait(bool value) => isWait = value;
    }
}