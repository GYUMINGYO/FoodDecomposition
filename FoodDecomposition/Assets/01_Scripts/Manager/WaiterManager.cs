using System.Collections.Generic;
using System.Linq;
using GM._01_Scripts.Data;
using GM.Staffs;
using UnityEngine;
using MKDir;
using UnityEditor.Rendering.Universal.ShaderGUI;

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

            //! Test
            waiterList.Add(FindAnyObjectByType<Waiter>());
        }

        private void Update()
        {
            GiveWork();
        }

        private void GiveWork()
        {
            // TODO : counter는 연속 처리 되게 만들어야 함
            if (_counterList.Count > 0)
            {
                CheckWorking()?.StartWork(WaiterState.COUNT);
            }

            if (_orderList.Count > 0)
            {
                CheckWorking()?.StartWork(WaiterState.ORDER);
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
    }
}