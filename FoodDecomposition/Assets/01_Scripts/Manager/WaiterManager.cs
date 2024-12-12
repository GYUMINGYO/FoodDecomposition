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
        private Queue<OrderData> _orderList;
        private Queue<Customer> _counterList;

        protected override void Awake()
        {
            base.Awake();
            waiterList = new List<Waiter>();
            _orderList = new Queue<OrderData>();
            _counterList = new Queue<Customer>();
        }

        public void AddOrderList(OrderData data)
        {
            _orderList.Enqueue(data);
        }

        public OrderData GetOrderData()
        {
            if (_orderList.Count <= 0)
            {
                return default;
            }

            return _orderList.Dequeue();
        }

        public void AddCounterList(Customer customer)
        {
            _counterList.Enqueue(customer);
        }

        public Customer GetCounterCustomer()
        {
            if (_counterList.Count <= 0)
            {
                return default;
            }

            return _counterList.Dequeue();
        }
    }
}