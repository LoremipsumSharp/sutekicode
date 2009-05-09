using System;
using System.Collections;
using System.ServiceModel.Channels;
using Castle.Facilities.WcfIntegration.Behaviors;

namespace Suteki.Blog.ConsoleService.Wcf
{
    public class LifestyleMessageAction : AbstractMessageAction
    {
        private readonly string stateToken = Guid.NewGuid().ToString();

        public LifestyleMessageAction()
            : base(MessageLifecycle.All)
        {
        }

        public override bool Perform(ref Message message, MessageLifecycle lifecycle, IDictionary state)
        {
            if (lifecycle == MessageLifecycle.IncomingRequest)
            {
                state.Add(stateToken, "This state has been stored for the duration of the request");
            }
            if (lifecycle == MessageLifecycle.OutgoingResponse)
            {
                Console.WriteLine(state[stateToken]);
            }

            Console.WriteLine("Perform called at lifecycle: {0}", lifecycle);
            return true;
        }
    }
}