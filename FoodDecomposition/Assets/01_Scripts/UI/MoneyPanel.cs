using System;
using TMPro;
using UnityEngine;

namespace GM.Managers
{
    public class MoneyPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyText;

        public void Awake()
        {
            SetMoneyUI(0);
        }

        private void Start()
        {
            DataManager dataManager = ManagerHub.Instance.GetManager<DataManager>();
            dataManager.OnChangeMoenyData += HandleOnChangeMoneyData;
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