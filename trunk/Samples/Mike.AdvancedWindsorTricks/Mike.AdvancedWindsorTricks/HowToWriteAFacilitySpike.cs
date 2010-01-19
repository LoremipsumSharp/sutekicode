using System;
using Castle.Facilities.Startable;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Mike.AdvancedWindsorTricks.Model;
using NUnit.Framework;

namespace Mike.AdvancedWindsorTricks
{
    public class HowToWriteAFacilitySpike
    {
        public void EventSubscriptionFacility_should_wire_up_events()
        {
            // TODO: Make it work for one subscriber with multiple subscriptions

            var container = new WindsorContainer()
                .AddFacility<StartableFacility>()
                .AddFacility<EventSubscriptionFacility>()
                .Register(
                    Component.For<TypedMessagePublisher>(),
//                    Component.For<AlternativeEventPublisher>(),
                    Component.For<NewCustomerSubscriber>()
                        .SubscribesTo().Event<Action<NewCustomerEvent>>().WithMethod(s => s.Handle)
//                        .SubscribesTo().Event<Action<AlternativeEvent>>().WithMethod(s => s.HandleAlternative)
                );

            var subscriber = container.Resolve<NewCustomerSubscriber>();
            Assert.That(!subscriber.NewCustomerWasHandled);

            var publisher = container.Resolve<TypedMessagePublisher>();
            publisher.CreateNewCustomer("Sim", 42);

            Assert.That(subscriber.NewCustomerWasHandled, "NewCustomerEvent was not handled");
            Assert.That(subscriber.CustomerName, Is.EqualTo("Sim"));
            Assert.That(subscriber.CustomerAge, Is.EqualTo(42));

//            var alternativePublisher = container.Resolve<AlternativeEventPublisher>();
//            var alternativeEvent = new AlternativeEvent();
//            alternativePublisher.RaiseAlternativeEvent(alternativeEvent);
//            Assert.That(subscriber.AlternativeEvent, Is.SameAs(alternativeEvent));
        }
    }
}