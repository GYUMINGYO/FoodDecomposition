using GM.Managers;
using UnityEngine;

namespace GM
{
    public class CustomerAnimator : MonoBehaviour
    {
        [SerializeField] private MoneyText moneyText;
        private Customer customer;

        private void Awake()
        {
            customer = GetComponentInParent<Customer>();
        }

        public void Count()
        {
            float price = customer.GetOrderData().recipe.sellPrice;
            ManagerHub.Instance.GetManager<DataManager>().AddMoney(price);
            moneyText.UpText(price);
        }
    }
}
