using GM.Data;
using GM.Staffs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GM.Managers
{
    public class StaffManager : IManagerable, IManagerUpdateable
    {
        private List<Staff> _staffList;
        private LinkedList<OrderData> _orderList;
        private Queue<OrderData> _recipeList;

        public void Initialized()
        {
            _staffList = new List<Staff>();
            _orderList = new LinkedList<OrderData>();
            _recipeList = new Queue<OrderData>();

            foreach (var staff in MonoBehaviour.FindObjectsByType<Staff>(FindObjectsSortMode.None))
            {
                _staffList.Add(staff);
            }
        }

        public void Clear()
        {
            _staffList.Clear();
            _orderList.Clear();
            _recipeList.Clear();
        }

        public void Update()
        {
            if (_staffList.Count <= 0) return;

            ChefGiveWork();
            WaiterGiveWork();
        }

        /// <summary>
        /// Add Data for WaiterManager Order
        /// </summary>
        /// <param name="data">Order data</param>
        public void AddOrderData(OrderData data)
        {
            Debug.Assert(data.type != OrderType.Null, "OrderData Type is Null");

            if (data.type == OrderType.Cook)
            {
                _recipeList.Enqueue(data);
            }

            _orderList.AddLast(data);
        }

        public void AddStaff(Staff staff)
        {
            _staffList.Add(staff);
        }

        public void RemoveStaff(Staff staff)
        {
            _staffList.Remove(staff);
        }

        private void ChefGiveWork()
        {
            if (_recipeList.Count <= 0) return;

            if (_recipeList.Count > 0)
            {
                CheckWorking<Chef>()?.StartWork(ChefState.COOK, _recipeList.Dequeue());
            }
        }

        private void WaiterGiveWork()
        {
            if (_orderList.Count <= 0) return;

            Waiter waiter = CheckWorking<Waiter>();
            if (waiter == default) return;

            foreach (OrderType workType in waiter.WorkPriority)
            {
                if (waiter.IsWorking == true) break;

                // Continuous calculation processing 
                if (workType == OrderType.Count)
                {
                    waiter = _staffList.OfType<Waiter>().FirstOrDefault(x => x.IsWorking == false && x.currentWaiterState == WaiterState.COUNT) ?? waiter;
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

        private T CheckWorking<T>() where T : Staff =>
            _staffList.OfType<T>().FirstOrDefault(x => x.IsWorking == false);
    }
}
