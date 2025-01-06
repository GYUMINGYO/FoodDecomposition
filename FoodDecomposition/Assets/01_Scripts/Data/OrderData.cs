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
        public Table orderTable;
        public Customer orderCustomer;
        public OrderType type;
        public Recipe recipe;
    }
}