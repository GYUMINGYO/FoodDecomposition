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
        private Queue<OrderData> _counterList;

        public void Init()
        {
            waiterList = new List<Waiter>();
            _orderList = new Queue<OrderData>();
            _counterList = new Queue<OrderData>();

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
                CheckWorking()?.StartWork(WaiterState.COUNT, GetOrderData(OrderDataType.Count));
            }

            if (_orderList.Count > 0)
            {
                CheckWorking()?.StartWork(WaiterState.ORDER, GetOrderData(OrderDataType.Order));
            }
        }

        private Waiter CheckWorking() =>
            waiterList.FirstOrDefault(x => x.IsWorking == false);

        public OrderData GetOrderData(OrderDataType type)
        {
            switch (type)
            {
                case OrderDataType.Order:
                    return GetOrderListData(_orderList);
                case OrderDataType.Count:
                    return GetOrderListData(_counterList);
                default:
                    return default;
            }
        }

        /// <summary>
        /// Get Data for WaiterManager Field Queue
        /// </summary>
        /// <param name="list">Ordet data Queue</param>
        /// <returns></returns>
        private OrderData GetOrderListData(Queue<OrderData> list)
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
        public void AddOrderData(OrderData data)
        {
            switch (data.type)
            {
                case OrderDataType.Order:
                    _orderList.Enqueue(data);
                    break;
                case OrderDataType.Serving:
                    // TODO : Serving 처리
                    break;
                case OrderDataType.Count:
                    _counterList.Enqueue(data);
                    break;
            }
        }

        public void Clear()
        {
            waiterList.Clear();
            _orderList.Clear();
            _counterList.Clear();
        }
    }
}
