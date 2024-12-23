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
                Customer counterCustomer = ManagerHub.WaiterManager.GetCounterData();
                if (isTwo)
                {
                    counterCustomer = ManagerHub.WaiterManager.GetCounterData();
                    float sellPrice = counterCustomer.GetSellPrice();
                    Debug.Log($"+{sellPrice}");
                }
                else
                {
                    counterCustomer = ManagerHub.WaiterManager.GetOrderData().orderCustomer;
                    isTwo = true;
                }

                counterCustomer.SetWait(false);
            }
        }
    }
}
