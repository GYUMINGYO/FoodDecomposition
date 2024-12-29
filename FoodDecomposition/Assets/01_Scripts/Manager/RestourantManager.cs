using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GM.Managers
{
    public class RestourantManager : IManagerable
    {
        public Dictionary<Transform, Customer> chairDictionary;

        public void Initialized()
        {
            chairDictionary = new Dictionary<Transform, Customer>();

            foreach (var chair in GameObject.FindGameObjectsWithTag("Chair"))
            {
                chairDictionary.Add(chair.transform, null);
            }
        }

        public void Clear()
        {
            chairDictionary.Clear();
        }

        public Transform GetChiar()
        {
            List<Transform> nullValueList = new List<Transform>();
            foreach(var pair in chairDictionary)
            {
                if(pair.Value == null)
                {
                    nullValueList.Add(pair.Key);
                }
            }

            if (nullValueList.Count == 0)
                return default;

            int randIdx = Random.Range(0, nullValueList.Count);
            return nullValueList[randIdx];
        }
    }
}
