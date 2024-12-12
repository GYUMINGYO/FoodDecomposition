using System.Collections.Generic;
using System.Linq;
using MKDir;
using UnityEngine;

namespace GM.Manager
{
    public class RestourantManager : MonoSingleton<RestourantManager>
    {
        public Dictionary<Transform, Customer> chairDictionary;

        protected override void Awake()
        {
            base.Awake();
            chairDictionary = new Dictionary<Transform, Customer>();
        }

        public void AddChiar(Transform chiarTrm)
        {
            chairDictionary.Add(chiarTrm, null);
        }

        public Transform GetChiar()
        {
            return chairDictionary.FirstOrDefault(x => x.Value == null).Key;
        }
    }
}
