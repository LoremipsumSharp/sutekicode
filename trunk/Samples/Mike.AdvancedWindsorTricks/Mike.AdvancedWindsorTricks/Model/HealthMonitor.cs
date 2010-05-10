using System;
using System.Linq;
using Castle.Core.Configuration;
using Castle.MicroKernel.SubSystems.Conversion;

namespace Mike.AdvancedWindsorTricks.Model
{
    public interface IHealthMonitor
    {
        HealthEndpoint[] HealthEndpoints { get; }
    }

    public class HealthMonitor : IHealthMonitor
    {
        public HealthEndpoint[] HealthEndpoints { get; private set; }

        public HealthMonitor(HealthEndpoint[] healthEndpoints)
        {
            HealthEndpoints = healthEndpoints;
        }
    }

    public class HealthEndpoint
    {
        public string Url { get; private set; }
        public string Expect { get; private set; }
        public int TimeoutSeconds { get; private set; }

        public HealthEndpoint(string url, string expect, int timeoutSeconds)
        {
            Url = url;
            Expect = expect;
            TimeoutSeconds = timeoutSeconds;
        }
    }

    public class HealthEndpointTypeConverter : AbstractTypeConverter
    {
        public override bool CanHandleType(Type type)
        {
            return type == typeof (HealthEndpoint);
        }

        public override object PerformConversion(string value, Type targetType)
        {
            throw new NotImplementedException();
        }

        public override object PerformConversion(IConfiguration configuration, Type targetType)
        {
            var converter = new Converter(configuration.Children, Context);
            var url = converter.Get<string>("url");
            var expect = converter.Get<string>("expect");
            var timeoutSeconds = converter.Get<int>("timeoutSeconds");

            return new HealthEndpoint(url, expect, timeoutSeconds);
        }

        private class Converter
        {
            private readonly ConfigurationCollection configurationCollection;
            private readonly ITypeConverterContext context;

            public Converter(ConfigurationCollection configurationCollection, ITypeConverterContext context)
            {
                this.configurationCollection = configurationCollection;
                this.context = context;
            }

            public T Get<T>(string paramter)
            {
                var configuration =  configurationCollection.SingleOrDefault(c => c.Name == paramter);
                if (configuration == null)
                {
                    throw new ApplicationException(string.Format(
                        "In the castle configuration, type '{0}' expects parameter '{1}' that was missing.",
                        typeof(T).Name, paramter));
                }
                return (T) context.Composition.PerformConversion(configuration, typeof (T));
            }
        }
    }
}