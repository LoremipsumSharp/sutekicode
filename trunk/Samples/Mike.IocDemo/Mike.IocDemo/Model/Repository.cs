using System;
using System.Collections.Generic;

namespace Mike.IocDemo.Model
{
    public class Repository<T> : IRepository<T> where T : new()
    {
        public IEnumerable<T> GetAll()
        {
            Console.WriteLine("Using Repository<T>");
            return new[]
                       {
                           new T(),
                           new T() 
                       };
        }
    }
}
