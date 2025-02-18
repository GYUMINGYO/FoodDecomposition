using GM.Managers;
using System;

namespace GM
{
    public class DataManager : IManagerable
    {
        public Action<float> OnChangeMoenyData;

        private int customerCnt;
        private float money;

        public int CustomerCnt => customerCnt;
        public float Money => money;

        public void Initialized()
        {
            customerCnt = 0;
            money = 0;
        }

        public void AddMoney(float money)
        {
            this.money += money;
            OnChangeMoenyData?.Invoke(this.money);
        }

        public void SubtractMoney(float money)
        {
            this.money -= money;
            OnChangeMoenyData?.Invoke(this.money);
        }

        public void AddCustomerCnt() => customerCnt++;

        public void SubtractCustomerCnt() => customerCnt--;

        public void Clear()
        {
        }
    }
}
