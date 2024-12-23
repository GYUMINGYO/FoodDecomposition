using GM.Managers;
using UnityEngine;

namespace GM
{
    public class TestScript : MonoBehaviour
    {
        private bool isTwo = false;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Customer counterCustomer;
                if (isTwo)
                {
                    counterCustomer = ManagerHub.WaiterManager.DequeueOrderData(Data.OrderType.Count).orderCustomer;
                    float sellPrice = counterCustomer.GetSellPrice();
                    Debug.Log($"+{sellPrice}");
                }
                else
                {
                    counterCustomer = ManagerHub.WaiterManager.DequeueOrderData(Data.OrderType.Order).orderCustomer;
                    isTwo = true;
                }

                counterCustomer.SetWait(false);
            }
        }
    }
}
