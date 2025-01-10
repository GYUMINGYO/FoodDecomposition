using UnityEngine;

namespace GM.Data
{
    public enum OrderType
    {
        Null,
        Order,
        Count,
        Serving
    }

    public class OrderData
    {
        public Table orderTable;
        public Customer orderCustomer;
        public OrderType type;
        public Recipe recipe;
        public bool isCustomerOut;
    }
}