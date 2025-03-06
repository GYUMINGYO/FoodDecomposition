using GM.Managers;
using UnityEngine;

namespace GM
{
    public class PreferenceManager : IManagerable
    {
        private int level = 0;
        private float preferenceGain;
        private float preferenceLossRate;
        private float targetPreference;
        private float totalPreference;

        public int Level => level;
        public float TargetPreference => targetPreference;
        public float TotalPreference => totalPreference;

        public void AddPreferenceGain(float amount)
        {
            preferenceGain += amount;
        }

        public void AddPreferenceLossRate(float rate)
        {
            preferenceLossRate += rate;
        }

        public void ApplyFinalPreference()
        {
            totalPreference += preferenceGain - preferenceGain * (preferenceLossRate / 100);
            preferenceGain = 0;
            preferenceLossRate = 0;
        }

        public void PreferenceLevelUp()
        {
            totalPreference = totalPreference - targetPreference;
            targetPreference = Mathf.Pow((level++ + 1) * 50 / 49, 2.5f) * 100;
        }

        public void Clear()
        {
        }

        public void Initialized()
        {
            PreferenceLevelUp();
        }
    }
}
