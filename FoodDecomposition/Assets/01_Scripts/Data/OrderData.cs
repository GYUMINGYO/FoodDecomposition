using UnityEngine;

namespace GM.Data
{
    public enum OrderType
    {
        Null,
        Order,
        Count,
        Serving,
        Cook
    }

    public class OrderData
    {
        public uint orderTableID;
        public Customer orderCustomer;
        public OrderType type;
        public RecipeSO recipe;
        public bool isCustomerOut;
    }
}