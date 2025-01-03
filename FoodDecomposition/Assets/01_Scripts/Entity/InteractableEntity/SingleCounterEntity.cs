using GM.CookWare;
using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    public class SingleCounterEntity : InteractableEntity
    {
        private Dictionary<Customer, int> lineDictionary = new();

        public Transform SenderTransform => _senderTransform;
        [SerializeField] protected Transform _senderTransform;

        [SerializeField] private List<Transform> lineTrmList;

        int lineIdx = 0;

        public Transform GetLineTrm(Customer customer)
        {
            if (lineIdx >= lineTrmList.Count)
            {
                return null;
            }

            lineDictionary[customer] = lineIdx + 1;
            return lineTrmList[lineIdx++];
        }

        public void ReleaseLine(Customer customer)
        {
            lineDictionary.Remove(customer);

            foreach (var key in lineDictionary.Keys)
            {
                lineDictionary[key]--;
                key.UpdateLine(lineTrmList[lineDictionary[key]]);
            }
        }
    }
}
