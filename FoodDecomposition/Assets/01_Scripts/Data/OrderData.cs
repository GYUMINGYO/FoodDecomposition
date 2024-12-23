namespace GM.Data
{
    public enum OrderType
    {
        Null,
        Order,
        Count,
        Serving
    }

    public struct OrderData
    {
        public Customer orderCustomer;
        public OrderType type;
        public Recipe recipe;
    }
}