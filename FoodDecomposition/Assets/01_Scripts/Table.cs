using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    public class Table : MonoBehaviour
    {
        public Dictionary<Transform, Customer> chairDictionary;

        private void Awake()
        {
            chairDictionary = new Dictionary<Transform, Customer>();
        }
    }
}