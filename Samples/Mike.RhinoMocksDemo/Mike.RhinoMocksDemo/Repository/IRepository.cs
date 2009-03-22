using System.Linq;

namespace Mike.RhinoMocksDemo.Repository
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        TEntity GetById(int id);
        IQueryable<TEntity> GetAll();
        void SaveOrUpdate(TEntity entity);
    }
}