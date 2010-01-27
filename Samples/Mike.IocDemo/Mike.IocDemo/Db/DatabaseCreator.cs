using System;
using System.Data.SqlClient;

namespace Mike.IocDemo.Db
{
    public class DatabaseCreator
    {
        public const string ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=master;Integrated Security=SSPI;";
        private const string CreateDb = @"create database IocDemo";
        private const string DropDb = @"drop database IocDemo";
        private const string CreateTable = 
@"use IocDemo
create table Report (
    Id int identity(1,1),
    Text varchar(50),
    ShouldSend bit
)";

        private const string InsertReportRow = @"insert IocDemo..Report(Text, ShouldSend) values(@Text, @ShouldSend)";

        /// <summary>
        /// Run this method using TestDriven.NET to create the database.
        /// </summary>
        public void Create()
        {
            ExecuteCommand(CreateDb);
            ExecuteCommand(CreateTable);
            AddReport("Report One", true);
            AddReport("Report Two", false);
            AddReport("Report Three", true);
        }

        public void Drop()
        {
            ExecuteCommand(DropDb);
        }

        public void ExecuteCommand(string commandText)
        {
            ExecuteCommand(c => c.CommandText = commandText);
        }

        public void ExecuteCommand(Action<SqlCommand> prepareCommand)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    prepareCommand(command);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddReport(string text, bool shouldSend)
        {
            ExecuteCommand(c =>
            {
                c.CommandText = InsertReportRow;
                c.Parameters.AddWithValue("Text", text);
                c.Parameters.AddWithValue("ShouldSend", shouldSend);
            });
        }
    }
}