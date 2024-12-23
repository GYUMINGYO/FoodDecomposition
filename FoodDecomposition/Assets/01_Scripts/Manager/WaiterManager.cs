using System.Collections.Generic;
using System.Linq;
using GM.Data;
using UnityEngine;
using GM.Staffs;

namespace GM.Managers
{
    public class WaiterManager : IManagerable, IManagerUpdateable
    {
        public List<Waiter> waiterList;
        private Queue<OrderData> _orderList;
        private Queue<OrderData> _counterList;

        public void Initialized()
        {
            waiterList = new List<Waiter>();
            _orderList = new Queue<OrderData>();
            _counterList = new Queue<OrderData>();

            foreach (var waiter in ManagerHub.FindObjectsByType<Waiter>(FindObjectsSortMode.None))
            {
                waiterList.Add(waiter);
            }
        }

        public void Clear()
        {
            waiterList.Clear();
            _orderList.Clear();
            _counterList.Clear();
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
                CheckWorking()?.StartWork(WaiterState.COUNT, DequeueOrderData(OrderType.Count));
            }

            if (_orderList.Count > 0)
            {
                CheckWorking()?.StartWork(WaiterState.ORDER, DequeueOrderData(OrderType.Order));
            }
        }

        private Waiter CheckWorking() =>
            waiterList.FirstOrDefault(x => x.IsWorking == false);

        public OrderData DequeueOrderData(OrderType type)
        {
            switch (type)
            {
                case OrderType.Order:
                    return DequeueOrderListData(_orderList);
                case OrderType.Count:
                    return DequeueOrderListData(_counterList);
                default:
                    return default;
            }
        }

        /// <summary>
        /// Get Data for WaiterManager Field Queue
        /// </summary>
        /// <param name="list">Ordet data Queue</param>
        /// <returns></returns>
        private OrderData DequeueOrderListData(Queue<OrderData> list)
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
            Debug.Assert(data.type != OrderType.Null, "OrderData Type is Null");

            switch (data.type)
            {
                case OrderType.Order:
                    _orderList.Enqueue(data);
                    break;
                case OrderType.Serving:
                    // TODO : Serving 처리
                    break;
                case OrderType.Count:
                    _counterList.Enqueue(data);
                    break;
            }
        }
    }
}
