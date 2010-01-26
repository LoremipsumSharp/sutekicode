using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Mike.IocDemo.Db;

namespace Mike.IocDemo.Model
{
    public class ReportRepository : IRepository<Report>
    {
        private const string GetReportsSQL = @"select Text, ShouldSend from IocDemo..Report";

        public IEnumerable<Report> GetAll()
        {
            var reports = new List<Report>();

            using (var connection = new SqlConnection(DatabaseCreator.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = GetReportsSQL;
                    using(var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reports.Add(new Report
                                            {
                                                Text = reader.GetString(0),
                                                ShouldSend = reader.GetBoolean(1)
                                            });
                        }
                    }
                }
            }

            return reports;
        }
    }

    public class ReportRepositoryTests
    {
        public void should_be_able_to_get_reports_from_db()
        {
            var reportRepository = new ReportRepository();
            foreach (var report in reportRepository.GetAll())
            {
                Console.Out.WriteLine("{0}, Send? {1}", report.Text, report.ShouldSend);
            }
        }        
    }
}
