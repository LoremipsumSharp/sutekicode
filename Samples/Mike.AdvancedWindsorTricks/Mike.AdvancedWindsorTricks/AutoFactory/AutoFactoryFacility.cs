using Castle.Core.Configuration;
using Castle.MicroKernel;

namespace Mike.AdvancedWindsorTricks.AutoFactory
{
    public class AutoFactoryFacility : IFacility
    {
        public void Init(IKernel kernel, IConfiguration facilityConfig)
        {
            kernel.Resolver.AddSubResolver(new AutoFactoryResolver(kernel));
        }

        public void Terminate(){}
    }
}