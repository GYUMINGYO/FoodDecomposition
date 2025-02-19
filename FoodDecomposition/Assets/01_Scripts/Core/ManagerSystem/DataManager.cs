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
        private float presonalCost;

        public int CustomerCnt => customerCnt;
        public float Money => money;
        public float DayMoney => dayMoney;
        public float MaterialCost => materialCost;
        public float PresonalCost => presonalCost;

        public void Initialized()
        {
            customerCnt = 0;
            money = 0;

            DayMoneysClear();
        }

        public void AddMoney(float money)
        {
            this.money += money;
            dayMoney += money;
            OnChangeMoenyData?.Invoke(this.money);
        }

        public void SubtractMoney(float money, SubMoneyType subMoneyType = SubMoneyType.None)
        {
            this.money -= money;
            OnChangeMoenyData?.Invoke(this.money);

            switch (subMoneyType)
            {
                case SubMoneyType.materialCost:
                    materialCost -= money;
                    break;
                case SubMoneyType.personalCost:
                    presonalCost -= money;
                    break;
            }
        }

        public void AddCustomerCnt() => customerCnt++;

        public void SubtractCustomerCnt() => customerCnt--;

        public void DayMoneysClear()
        {
            dayMoney = 0;
            materialCost = 0;
            presonalCost = 0;
        }

        public void Clear()
        {
        }
    }
}
