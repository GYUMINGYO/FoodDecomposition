using GM.Managers;
using UnityEngine;

namespace GM
{
    public class TestScript : MonoBehaviour
    {
        private bool isTwo = false;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                if (isTwo)
                {
                    Customer counterCustomer = ManagerHub.Instance.GetManager<WaiterManager>().DequeueOrderData(Data.OrderType.Count).orderCustomer;

                    float sellPrice = counterCustomer.GetSellPrice();
                    Debug.Log($"+{sellPrice}");

                    counterCustomer.SetWait(false);
                }
                else
                {
                    Customer counterCustomer = ManagerHub.Instance.GetManager<WaiterManager>().DequeueOrderData(Data.OrderType.Order).orderCustomer;
                    isTwo = true;

                    counterCustomer.SetWait(false);
                }
            }
        }
    }
}
