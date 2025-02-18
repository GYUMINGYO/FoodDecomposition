using GM.Managers;
using System;

namespace GM
{
    public class DataManager : IManagerable
    {
        public Action<float> OnChangeMoenyData;

        private float money;
        public float Money => money;

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

        public void Clear()
        {
        }

        public void Initialized()
        {
        }
    }
}
