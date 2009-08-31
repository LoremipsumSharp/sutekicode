namespace Mike.NHibernateDemo.Model
{
    public class OrderLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public Order Order { get; set; }

        public decimal GetTotalPrice()
        {
            return Product.Price*Quantity;
        }
    }
}