using GM.Data;
using GM.Staffs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GM.Managers
{
    public class WaiterManager : IManagerable, IManagerUpdateable
    {
        private List<Waiter> _waiterList;
        private LinkedList<OrderData> _orderList;

        private bool isTest = false;

        public void Initialized()
        {
            _waiterList = new List<Waiter>();
            _orderList = new LinkedList<OrderData>();

            foreach (var waiter in MonoBehaviour.FindObjectsByType<Waiter>(FindObjectsSortMode.None))
            {
                _waiterList.Add(waiter);
            }
        }

        public void Clear()
        {
            _waiterList.Clear();
            _orderList.Clear();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                isTest = !isTest;
            }

            GiveWork();
        }

        /// <summary>
        /// Add Data for WaiterManager Order
        /// </summary>
        /// <param name="data">Order data</param>
        public void AddOrderData(OrderData data)
        {
            Debug.Assert(data.type != OrderType.Null, "OrderData Type is Null");

            _orderList.AddLast(data);
        }

        private void GiveWork()
        {
            if (_waiterList.Count <= 0 || _orderList.Count <= 0) return;

            Waiter waiter = CheckWorking();
            if (waiter == default) return;

            foreach (OrderType workType in waiter.WorkPriority)
            {
                if (waiter.IsWorking == true) break;

                // Continuous calculation processing 
                if (workType == OrderType.Count)
                {
                    waiter = _waiterList.FirstOrDefault(x => x.IsWorking == false && x.currentWaiterState == WaiterState.COUNT) ?? waiter;
                }

                OrderData data = _orderList.FirstOrDefault(x => x.type == workType);
                if (data != default)
                {
                    switch (data.type)
                    {
                        case OrderType.Order:
                            waiter.StartWork(WaiterState.ORDER, data);
                            break;
                        case OrderType.Count:
                            waiter.StartWork(WaiterState.COUNT, data);
                            break;
                        case OrderType.Serving:
                            waiter.StartWork(WaiterState.SERVING, data);
                            break;
                    }
                    _orderList.Remove(data);
                }
            }
        }

        private Waiter CheckWorking() =>
            _waiterList.FirstOrDefault(x => x.IsWorking == false);
    }
}
