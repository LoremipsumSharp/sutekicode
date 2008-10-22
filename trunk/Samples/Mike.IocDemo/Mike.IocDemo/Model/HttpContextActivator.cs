using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ComponentActivator;

namespace Mike.IocDemo.Model
{
    public class HttpContextActivator : AbstractComponentActivator
    {
        public HttpContextActivator(
            ComponentModel model, 
            IKernel kernel, 
            ComponentInstanceDelegate onCreation, 
            ComponentInstanceDelegate onDestruction) : 
            base(model, kernel, onCreation, onDestruction)
        {
        }

        protected override object InternalCreate(CreationContext context)
        {
            return HttpContext.Current;
        }

        protected override void InternalDestroy(object instance)
        {
            // do nothing
        }
    }
}