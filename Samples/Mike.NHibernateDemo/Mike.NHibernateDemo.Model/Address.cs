namespace Mike.NHibernateDemo.Model
{
    public class Address : IComponent
    {
        public virtual string Line1 { get; set; }
        public virtual string Town { get; set; }
    }
}