using System.Collections.Generic;
using System.Linq;
using MKDir;
using UnityEditor.Build;
using UnityEngine;

namespace GM.Manager
{
    public class RestourantManager : MonoSingleton<RestourantManager>
    {
        public List<Transform> startChairList;
        public Dictionary<Transform, Customer> chairDictionary;

        protected override void Awake()
        {
            base.Awake();
            chairDictionary = new Dictionary<Transform, Customer>();
            startChairList.ForEach(chair => AddChiar(chair));
        }

        public void AddChiar(Transform chairTrm)
        {
            chairDictionary.Add(chairTrm, null);
        }

        public Transform GetChiar()
        {
            return chairDictionary.FirstOrDefault(x => x.Value == null).Key;
        }
    }
}
