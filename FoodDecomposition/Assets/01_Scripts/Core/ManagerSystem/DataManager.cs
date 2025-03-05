using GM.Managers;
using System;

public enum SubMoneyType
{
    materialCost,
    personalCost,
    None
}

namespace GM
{
    public class DataManager : IManagerable
    {
        public Action<float> OnChangeMoenyData;

        private int customerCnt;
        private float money;
        private float dayMoney;
        private float materialCost;
        private float personalCost;

        public int CustomerCnt => customerCnt;
        public float Money => money;
        public float DayMoney => dayMoney;
        public float MaterialCost => materialCost;
        public float PersonalCost => personalCost;

        public void Initialized()
        {
            customerCnt = 0;
            money = 50;

            DailyCostsClear();
        }

        public void AddMoney(float money)
        {
            this.money += money;
            dayMoney += money;
            OnChangeMoenyData?.Invoke(this.money);
        }

        public void SubtractMoney(float money)
        {
            this.money -= money;
            OnChangeMoenyData?.Invoke(this.money);
        }

        public void AddCustomerCnt() => customerCnt++;

        public void SubtractCustomerCnt() => customerCnt--;

        public void AddMaterialCost(float money)
        {
            materialCost += money;
        }

        public void AddPersonalCost(float money)
        {
            personalCost += money;
        }

        public void DailyCostsClear()
        {
            SubtractMoney(materialCost + personalCost);

            dayMoney = 0;
            materialCost = 0;
            personalCost = 0;
        }

        public void Clear()
        {
        }
    }
}
