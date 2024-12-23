namespace GM.Data
{
    public enum OrderDataType
    {
        Null,
        Order,
        Count,
        Serving
    }

    public struct OrderData
    {
        public Customer orderCustomer;
        public OrderDataType type;
        public Recipe recipe;
    }
}