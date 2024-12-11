using System.Collections.Generic;
using GM._01_Scripts.Data;
using GM.Staffs;
using MKDir;
using UnityEngine;

namespace GM.Manager
{
    public class WaiterManager : MonoSingleton<WaiterManager>
    {
        public List<Waiter> waiterList;
        private Queue<OrderData> _orderData;

        protected override void Awake()
        {
            base.Awake();
            waiterList = new List<Waiter>();
            _orderData = new Queue<OrderData>();
        }

        public void AddOrderData(OrderData data)
        {
            _orderData.Enqueue(data);
        }

        public OrderData GetOrderData()
        {
            if (_orderData.Count <= 0)
            {
                return default;
            }

            return _orderData.Dequeue();
        }
    }
}