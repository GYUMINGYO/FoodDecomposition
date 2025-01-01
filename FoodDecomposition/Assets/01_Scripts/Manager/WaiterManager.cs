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
        private Queue<OrderData> _servingList;

        // TODO : 지금 생각해보니까 굳이 타입을 나눠서 저장할 필요도 없을 거 같은데
        // * 탐색만 잘해서 하면 되지 않나? => 링큐는 느리긴한데 -> 괜찮지 않을까? (그렇게 성능 저하가 심할려나?)

        public void Initialized()
        {
            waiterList = new List<Waiter>();
            _orderList = new Queue<OrderData>();
            _counterList = new Queue<OrderData>();
            _servingList = new Queue<OrderData>();

            foreach (var waiter in MonoBehaviour.FindObjectsByType<Waiter>(FindObjectsSortMode.None))
            {
                waiterList.Add(waiter);
            }
        }

        public void Clear()
        {
            waiterList.Clear();
            _orderList.Clear();
            _counterList.Clear();
            _servingList.Clear();
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
                var waiter = waiterList.Where(x => x.IsWorking == true && x.currentWaiterState == WaiterState.COUNT);
                if (waiter.Count() <= 0)
                {
                    // 현재 계산을 하고 있는 직원이 없다
                    CheckWorking()?.StartWork(WaiterState.COUNT, DequeueOrderData(OrderType.Count));
                }
                else
                {
                    if (waiter.First().IsWorking == false)
                    {
                        waiter.First().StartWork(WaiterState.COUNT, DequeueOrderData(OrderType.Count));
                    }
                }
            }

            if (_orderList.Count > 0)
            {
                CheckWorking()?.StartWork(WaiterState.ORDER, DequeueOrderData(OrderType.Order));
            }

            if (_servingList.Count > 0)
            {
                CheckWorking()?.StartWork(WaiterState.SERVING, DequeueOrderData(OrderType.Serving));
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
                case OrderType.Serving:
                    return DequeueOrderListData(_servingList);
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
                    _servingList.Enqueue(data);
                    break;
                case OrderType.Count:
                    _counterList.Enqueue(data);
                    break;
            }
        }
    }
}
