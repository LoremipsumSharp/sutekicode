using System.Collections.Generic;

namespace Mike.IocDemo.Model
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
    }
}
