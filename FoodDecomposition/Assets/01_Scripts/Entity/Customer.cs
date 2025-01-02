using GM.Data;
using GM.Entities;
using Unity.Behavior;
using UnityEngine;

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
            if (_orderData.Equals(default(OrderData)))
                return 0;
            else
                return _orderData.recipe.sellPrice;
        }

        public OrderData GetOrderData()
        {
            if (_orderData.Equals(default(OrderData)))
                return default;
            else
                return _orderData;
        }

        public void SetWait(bool value) => isWait = value;
    }
}