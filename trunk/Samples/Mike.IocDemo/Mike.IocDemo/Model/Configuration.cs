namespace Mike.IocDemo.Model
{
    public class Configuration
    {
        private readonly string name;
        private readonly string connectionString;

        public Configuration(string name, string connectionString)
        {
            this.name = name;
            this.connectionString = connectionString;
        }

        public string Name
        {
            get { return name; }
        }

        public string ConnectionString
        {
            get { return connectionString; }
        }
    }
}