namespace Mike.NHibernateDemo.Model
{
    public interface IEntity
    {
        int Id { get; }
        int Version { get; }
    }
}