using GM.Data;
using GM.Managers;
using UnityEngine;

namespace GM
{
    public class TestBT : MonoBehaviour
    {
        public Customer _customer;
        OrderData order;

        private void Start()
        {
            order = new OrderData();
            order.orderCustomer = _customer;
            order.type = OrderType.Order;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                ManagerHub.Instance.GetManager<WaiterManager>().AddOrderData(order);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                order.type = OrderType.Count;

                for (int i = 0; i < 3; ++i)
                {
                    ManagerHub.Instance.GetManager<WaiterManager>().AddOrderData(order);
                }
            }
        }
    }
}
