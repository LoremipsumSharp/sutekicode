namespace Mike.NHibernateDemo.Model
{
    public class Product : IEntity
    {
        public virtual int Id { get; protected set; }
        public virtual int Version { get; protected set; }
        public virtual string Name { get; set; }
        public virtual decimal Price { get; set; }
    }
}