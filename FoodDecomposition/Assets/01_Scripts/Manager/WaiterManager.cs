using System.Collections.Generic;
using GM._01_Scripts.Data;
using GM.Staffs;
using MKDir;

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

        /// <summary>
        /// Add Data for WaiterManager Field Queue
        /// </summary>
        /// <param name="data">Add Data</param>
        /// <param name="list">Save Queue</param>
        /// <typeparam name="T">type</typeparam>
        public void AddListData<T>(T data, Queue<T> list)
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

        public void AddListData(OrderData data)
        {
            AddListData(data, _orderList);
            // TODO : 여기 주문 처리 하기
            //* 이벤트를 발행
            //* 이벤트 채널을 각각 클로닝해서 갖고 있고 그 각각에 전해 줄까? 근데 할 때마다 데이터를 갖고 와야 해
            //* 아니면 이건 Waiter에 변수를 만들던지 해서 해결 하는게 깔끕할 듯 
        }

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