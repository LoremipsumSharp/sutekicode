using System;
using Castle.Facilities.EventWiring;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Mike.AdvancedWindsorTricks.Model;

namespace Mike.AdvancedWindsorTricks
{
    public class EventWiringSpike
    {
        public void event_wiring_without_container()
        {
            var publisher = new MessagePublisher();
            var subscriber = new MessageListener();

            publisher.MessagePublished += subscriber.OnMessagePublished;
            publisher.MessagePublished += message => Console.WriteLine("Me too {0}", message);

            publisher.PublishMessage("Hello World!");
        }

        public void event_wiring_should_work()
        {
            var container = new WindsorContainer()
                .AddFacility<EventWiringFacility>()
                .Register(
                    Component.For<MessagePublisher>()
                        .Configuration(
                            Child.ForName("subscribers").Eq(
                                Child.ForName("subscriber").Eq(
                                    Attrib.ForName("id").Eq("messageListener"),
                                    Attrib.ForName("event").Eq("MessagePublished"),
                                    Attrib.ForName("handler").Eq("OnMessagePublished")
                                    )
                                )
                            ),
                    Component.For<MessageListener>().Named("messageListener")
                );


            var publisher = container.Resolve<MessagePublisher>();
            publisher.PublishMessage("Hello World!");
        }
    }
}