using System;
using TMPro;
using UnityEngine;

namespace GM.Managers
{
    public class MoneyPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyText;

        private void Start()
        {
            DataManager dataManager = ManagerHub.Instance.GetManager<DataManager>();
            dataManager.OnChangeMoenyData += HandleOnChangeMoneyData;

            SetMoneyUI(dataManager.Money);
        }

        private void HandleOnChangeMoneyData(float money)
        {
            SetMoneyUI(money);
        }

        public void SetMoneyUI(float money)
        {
            moneyText.text = $"${money}";
        }
    }
}