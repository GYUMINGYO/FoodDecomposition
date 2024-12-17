using System.Collections.Generic;
using System.Linq;
using GM.Data;
using UnityEngine;

using GM.Staffs;

namespace GM.Managers
{
    public class WaiterManager
    {
        public List<Waiter> waiterList;
        private Queue<OrderData> _orderList;
        private Queue<Customer> _counterList;

        public void Init()
        {
            waiterList = new List<Waiter>();
            _orderList = new Queue<OrderData>();
            _counterList = new Queue<Customer>();

            foreach (var waiter in ManagerHub.FindObjectsByType<Waiter>(FindObjectsSortMode.None))
            {
                waiterList.Add(waiter);
            }
        }

        public void Update()
        {
            GiveWork();
        }

        private void GiveWork()
        {
            if (waiterList.Count <= 0) return;

            // TODO : counter는 연속 처리 되게 만들어야 함
            if (_counterList.Count > 0)
            {
                //CheckWorking()?.StartWork(WaiterState.COUNT);
            }

            if (_orderList.Count > 0)
            {
                CheckWorking()?.StartWork(WaiterState.ORDER, GetListData(_orderList));
                GetListData(_orderList);
            }
        }

        private Waiter CheckWorking() =>
            waiterList.FirstOrDefault(x => x.IsWorking == false);

        /// <summary>
        /// Add Data for WaiterManager Field Queue
        /// </summary>
        /// <param name="data">Add Data</param>
        /// <param name="list">Save Queue</param>
        /// <typeparam name="T">type</typeparam>
        private void AddListData<T>(T data, Queue<T> list)
        {
            list.Enqueue(data);
        }

        /// <summary>
        /// Get Data for WaiterManager Field Queue
        /// </summary>
        /// <param name="list">Get Queue</param>
        /// <typeparam name="T">type</typeparam>
        /// <returns></returns>
        public T GetListData<T>(Queue<T> list)
        {
            if (list.Count <= 0)
            {
                return default;
            }

            return list.Dequeue();
        }

        /// <summary>
        /// Add Data for WaiterManager Order
        /// </summary>
        /// <param name="data">Order data</param>
        public void AddListData(OrderData data)
        {
            AddListData(data, _orderList);
        }

        /// <summary>
        /// Add Data for WaiterManager count
        /// </summary>
        /// <param name="customer">Count data</param>
        public void AddListData(Customer customer)
        {
            AddListData(customer, _counterList);
        }

        public OrderData GetOrderData()
        {
            return GetListData(_orderList);
        }

        public Customer GetCounterData()
        {
            return GetListData(_counterList);
        }

        public void Clear()
        {
            waiterList.Clear();
            _orderList.Clear();
            _counterList.Clear();
        }
    }
}
