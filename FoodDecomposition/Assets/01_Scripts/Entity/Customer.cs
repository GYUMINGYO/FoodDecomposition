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
        Counter,
        Exit
    }

    public class Customer : Entity, IPoolable
    {
        [SerializeField] private PoolTypeSO poolType;

        private OrderData _orderData;
        private bool isWait = false;

        public bool IsWait => isWait;
        public PoolTypeSO PoolType => poolType;
        public GameObject GameObject => gameObject;

        public void SetOrderData(OrderData orderData) => _orderData = orderData;

        public float GetSellPrice() =>
            _orderData.Equals(default(OrderData)) ? 0 : _orderData.recipe.sellPrice;

        public OrderData GetOrderData() =>
            _orderData.Equals(default(OrderData)) ? default : _orderData;

        public void SetWait(bool value) => isWait = value;

        public void SetUpPool(Pool pool) { }

        public void ResetItem() 
        {
            SetWait(false);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}