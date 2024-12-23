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

            foreach (var chair in ManagerHub.FindObjectsByType<Chair>(FindObjectsSortMode.None))
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
            return chairDictionary.FirstOrDefault(x => x.Value == null).Key;
        }
    }
}
