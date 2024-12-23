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
            order.type = OrderDataType.Order;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                ManagerHub.WaiterManager.AddOrderData(order);
            }
        }
    }
}
