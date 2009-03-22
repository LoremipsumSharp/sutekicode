namespace Mike.RhinoMocksDemo.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public Name Name { get; set; }
        public Order CurrentOrder { get; set; }
    }
}