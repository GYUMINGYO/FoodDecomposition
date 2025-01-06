using GM.InteractableEntitys;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GM
{
    public class SingleCounterEntity : InteractableEntity
    {
        private List<Customer> lineCustomerList = new();

        public Transform SenderTransform => _senderTransform;
        [SerializeField] protected Transform _senderTransform;

        [SerializeField] private List<Transform> lineTrmList;

        int lineIdx = 0;

        private void Awake()
        {
            for(int i = 0; i < 3; ++i)
            {
                lineCustomerList.Add(null);
            }
        }

        public Transform GetLineTrm(Customer customer)
        {
            if (lineIdx >= lineCustomerList.Count)
            {
                return null;
            }

            lineCustomerList[lineIdx] = customer;
            return lineTrmList[lineIdx++];
        }

        public void ExitLine(Customer customer)
        {
            int idx = lineCustomerList.IndexOf(customer);

            if(idx >=0 )
                lineCustomerList[idx] = null;
            lineIdx--;
        }

        public Transform CheckEmptyFront(Customer customer)
        {
            int idx = lineCustomerList.IndexOf(customer);
            if (idx <= 0)
            {
                return null;
            }

            idx--;
            if (lineCustomerList[idx] == null)
            {
                lineCustomerList[idx] = customer;
                return lineTrmList[idx];
            }
            else
            {
                return null;
            }
        }
    }
}
