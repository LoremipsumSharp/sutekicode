using System.Collections.Generic;
using System.Linq;

namespace Mike.AdvancedWindsorTricks.Model
{
    public interface IEntity { int Id { get; } }

    public interface IRepository<TEntity> where TEntity : IEntity
    {
        TEntity Get(int id);
        IQueryable<TEntity> GetAll();
        void Save(TEntity entity);
        void Delete(TEntity entity);
    }

    /// <summary>
    /// A Generic repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        private readonly IDictionary<int, TEntity> entities = new Dictionary<int, TEntity>();

        public virtual TEntity Get(int id)
        {
            return entities[id];
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return entities.Values.AsQueryable();
        }

        public virtual void Save(TEntity entity)
        {
            entities.Add(entity.Id, entity);
        }

        public virtual void Delete(TEntity entity)
        {
            entities.Remove(entity.Id);
        }
    }

    public class CustomerRepository : Repository<Customer>
    {
        public override IQueryable<Customer> GetAll()
        {
            return base.GetAll().Where(c => c.IsActive);
        }
    }

    public class Customer : IEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }

    public class Product : IEntity
    {
        public int Id { get; set; }
    }

    public class Order : IEntity
    {
        public int Id { get; set; }
    }
}