using System.Collections.Generic;
using UnityEngine;

namespace GM.Managers
{
    public class RestourantManager : IManagerable
    {
        public Dictionary<Transform, Customer> chairDictionary;

        private Dictionary<CookingTableType, CookingTable> _cookingTableDictionary;

        public void Initialized()
        {
            chairDictionary = new Dictionary<Transform, Customer>();
            _cookingTableDictionary = new Dictionary<CookingTableType, CookingTable>();

            foreach (var chair in GameObject.FindGameObjectsWithTag("Chair"))
            {
                chairDictionary.Add(chair.transform, null);
            }

            foreach (var table in GameObject.FindObjectsByType<CookingTable>(FindObjectsSortMode.None))
            {
                _cookingTableDictionary.Add(table.Type, table);
            }
        }

        public void Clear()
        {
            chairDictionary.Clear();
        }

        public Transform GetChiar()
        {
            List<Transform> nullValueList = new List<Transform>();
            foreach (var pair in chairDictionary)
            {
                if (pair.Value == null)
                {
                    nullValueList.Add(pair.Key);
                }
            }

            if (nullValueList.Count == 0)
                return default;

            int randIdx = Random.Range(0, nullValueList.Count);
            return nullValueList[randIdx];
        }

        public CookingTable GetCookingTable(CookingTableType type)
        {
            if (_cookingTableDictionary.TryGetValue(type, out CookingTable table))
            {
                return table;
            }

            return default;
        }
    }
}
