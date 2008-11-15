using System;
using System.Collections.Generic;

namespace Mike.IocDemo.Model
{
    public class ReportRepository : IRepository<Report>
    {
        public IEnumerable<Report> GetAll()
        {
            Console.WriteLine("Using ReportRepository");
            return new[]
                       {
                           new Report(), 
                           new Report(), 
                           new Report(), 
                       };
        }
    }
}
