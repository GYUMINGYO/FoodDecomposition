namespace GM.Data
{
    public enum OrderType
    {
        Order,
        Serving,
        Count
    }

    public struct OrderData
    {
        public Customer orderCustomer;
        public OrderType type;
        public Recipe recipe;
    }
}