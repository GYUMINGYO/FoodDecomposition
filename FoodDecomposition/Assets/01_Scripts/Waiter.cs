using GM.Manager;
using UnityEngine;

namespace GM.Staffs
{
    public class Waiter : Staff
    {
        private void Update()
        {
            //�׽�Ʈ
            if(Input.GetKeyDown(KeyCode.P))
            {
                Customer counterCustomer = WaiterManager.Instance.GetCounterCustomer();
                float sellPrice = counterCustomer.GetSellPrice();
                Debug.Log($"+{sellPrice}");

                counterCustomer.ChangeState(CustomerState.CounterComplete);
            }
        }
    }
}