using GM.Managers;
using System.Collections.Generic;
using UnityEngine;

struct PreferenceData
{
    public float prefGain;
    public float prefGainRate;
    public float prefLossRate;
}

namespace GM
{
    public class PreferenceManager : IManagerable
    {
        private Dictionary<Customer, PreferenceData> prefDictionary = new();

        private int level = 0;
        private float targetPref;
        private float totalPref;

        public int Level => level;
        public float TargetPreference => targetPref;
        public float TotalPreference => totalPref;

        public void AddDictionary(Customer customer)
        {
            PreferenceData data = new PreferenceData();
            prefDictionary[customer] = data;
        }

        public void AddPreferenceGain(Customer customer, float amount)
        {
            PreferenceData data = prefDictionary[customer];
            data.prefGain += amount;
        }

        public void AddPreferenceGainRate(Customer customer, float amount)
        {
            PreferenceData data = prefDictionary[customer];
            data.prefGainRate += amount;
        }

        public void AddPreferenceLossRate(Customer customer, float rate)
        {
            PreferenceData data = prefDictionary[customer];
            data.prefLossRate += rate;
        }

        public void ApplyFinalPreference(Customer customer)
        {
            totalPref += prefDictionary[customer].prefGain - prefDictionary[customer].prefGain * (prefDictionary[customer].prefLossRate / 100);

            prefDictionary[customer] = new();
        }

        public void PreferenceLevelUp()
        {
            totalPref = totalPref - targetPref;
            targetPref = Mathf.Pow((level++ + 1) * 50 / 49, 2.5f) * 100;
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
