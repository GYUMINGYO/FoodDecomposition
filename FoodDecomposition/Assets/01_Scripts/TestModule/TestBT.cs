using GM.Data;
using GM.Managers;
using UnityEngine;

namespace GM
{
    public class TestBT : MonoBehaviour
    {
        public Customer _customer;
        OrderData order;

        private bool _isPress = false;

        private void Start()
        {
            order = new OrderData();
            order.orderCustomer = _customer;
            order.type = OrderType.Order;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (_isPress == true) return;
                _isPress = true;

                order = new OrderData();
                order.orderCustomer = _customer;
                order.type = OrderType.Order;
                ManagerHub.Instance.GetManager<StaffManager>().AddOrderData(order);
                _isPress = false;

            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                if (_isPress == true) return;
                _isPress = true;

                order.type = OrderType.Count;

                ManagerHub.Instance.GetManager<StaffManager>().AddOrderData(order);
                _isPress = false;

            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if (_isPress == true) return;
                _isPress = true;

                order.type = OrderType.Serving;

                ManagerHub.Instance.GetManager<StaffManager>().AddOrderData(order);
                _isPress = false;
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                if (_isPress == true) return;
                _isPress = true;

                order.type = OrderType.Cook;

                ManagerHub.Instance.GetManager<StaffManager>().AddOrderData(order);
                _isPress = false;
            }
        }
    }
}
