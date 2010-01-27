using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Mail;
using log4net;
using log4net.Config;
using Mike.IocDemo.Db;
using Mike.IocDemo.Model;

namespace Mike.IocDemo.Default
{
    public class Program
    {
        private const string GetReportsSQL = @"select Text, ShouldSend from IocDemo..Report";

        public static void Main()
        {
            BasicConfigurator.Configure();
            var log = LogManager.GetLogger(typeof(Program));

            var reports = new List<Report>();

            using (var connection = new SqlConnection(DatabaseCreator.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = GetReportsSQL;
                    using (var reader = command.ExecuteReader())
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

            var smtpClient = new SmtpClient();
            foreach (var report in reports)
            {
                if(report.ShouldSend)
                {
                    var message = new MailMessage("no-reply@suteki.co.uk", "big.boss@suteki.co.uk")
                    {
                        Subject = "Important report for you!",
                        Body = report.Text
                    };
                    smtpClient.Send(message);
                    log.Info(string.Format("Sent email {0}", report.Text));
                }
            }
        }
    }
}