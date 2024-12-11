using System.Collections.Generic;
using System.Linq;
using MKDir;
using UnityEngine;

namespace GM.Manager
{
    public class RestourantManager : MonoSingleton<RestourantManager>
    {
        public Dictionary<Transform, Customer> chiarDictionary;

        protected override void Awake()
        {
            base.Awake();
            chiarDictionary = new Dictionary<Transform, Customer>();
        }

        public void AddChiar(Transform chiarTrm)
        {
            chiarDictionary.Add(chiarTrm, null);
        }

        public Transform GetChiar()
        {
            return chiarDictionary.FirstOrDefault(x => x.Value == null).Key;
        }
    }
}
