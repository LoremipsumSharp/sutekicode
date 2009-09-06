namespace Mike.NHibernateDemo.Model
{
    public class OrderLine : IEntity
    {
        public virtual int Id { get; protected set; }

        public virtual Product Product { get; set; }
        public virtual int Quantity { get; set; }

        public virtual Order Order { get; set; }

        public virtual decimal GetTotalPrice()
        {
            return Product.Price*Quantity;
        }
    }
}