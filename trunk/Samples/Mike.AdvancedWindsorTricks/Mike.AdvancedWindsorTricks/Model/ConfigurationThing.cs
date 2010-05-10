namespace Mike.AdvancedWindsorTricks.Model
{
    public interface IConfigurationThing
    {
        string Server { get; set; }
        string Database { get; set; }
        string User { get; set; }
        string Password { get; set; }
    }

    public class ConfigurationThing : IConfigurationThing
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}