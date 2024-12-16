using GM._01_Scripts.Data;
using GM.Manager;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GM
{
    public class TestBT : MonoBehaviour
    {
        OrderData order;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                WaiterManager.Instance.AddListData(order);
            }
        }
    }
}
