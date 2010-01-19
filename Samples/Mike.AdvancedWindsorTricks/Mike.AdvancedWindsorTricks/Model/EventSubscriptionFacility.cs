using System;
using System.Collections.Generic;
using Castle.Core;
using Castle.Core.Configuration;
using Castle.DynamicProxy;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

namespace Mike.AdvancedWindsorTricks.Model
{
    public class EventSubscriptionFacility : IFacility
    {
        public const string SubscriptionPropertyKey = "__event_subscription__";
        private readonly IDictionary<Type, EventSubscriptionInfo> subscriptionConfigs = 
            new Dictionary<Type, EventSubscriptionInfo>();
        private IKernel kernel;

        public void Init(IKernel kernel, IConfiguration facilityConfig)
        {
            this.kernel = kernel;
            kernel.ComponentModelCreated += KernelOnComponentModelCreated;
            kernel.ComponentCreated += KernelOnComponentCreated;
        }

        private void KernelOnComponentModelCreated(ComponentModel model)
        {
            if(model.ExtendedProperties.Contains(SubscriptionPropertyKey))
            {
                var subscriptionConfig = (EventSubscriptionFacilityConfig)
                    model.ExtendedProperties[SubscriptionPropertyKey];
                subscriptionConfigs.Add(subscriptionConfig.EventType, 
                    new EventSubscriptionInfo(model.Name, model.Service, subscriptionConfig.HandlerProvider));
            }
        }

        private void KernelOnComponentCreated(ComponentModel model, object instance)
        {
            foreach (var eventInfo in model.Implementation.GetEvents())
            {
                if (subscriptionConfigs.ContainsKey(eventInfo.EventHandlerType))
                {
                    var eventSubscriptionInfo = subscriptionConfigs[eventInfo.EventHandlerType];
                    var subscriber = kernel.Resolve(
                        eventSubscriptionInfo.ComponentId, 
                        eventSubscriptionInfo.ServiceType);

                    var handler = (Delegate)eventSubscriptionInfo.HandlerProvider.DynamicInvoke(subscriber);
                    eventInfo.AddEventHandler(instance, handler);
                }
            }
        }

        public void Terminate()
        {
        }
    }

    public class EventSubscriptionInfo
    {
        public string ComponentId { get; private set; }
        public Type ServiceType { get; private set;  }
        public Delegate HandlerProvider { get; private set; }

        public EventSubscriptionInfo(string componentId, Type serviceType, Delegate handlerProvider)
        {
            ComponentId = componentId;
            ServiceType = serviceType;
            HandlerProvider = handlerProvider;
        }
    }

    public static class EventSubscriptionFacilityConfigurationExtensions
    {
        public static SubscriptionRegistration<TComponent> SubscribesTo<TComponent>(
            this ComponentRegistration<TComponent> registration)
        {
            return new SubscriptionRegistration<TComponent>(registration);
        }
    }

    public class SubscriptionRegistration<TComponent>
    {
        private readonly ComponentRegistration<TComponent> componentRegistration;

        public SubscriptionRegistration(ComponentRegistration<TComponent> componentRegistration)
        {
            this.componentRegistration = componentRegistration;
        }

        public SubscriptionRegistrationForMethod<TComponent, TEvent> Event<TEvent>()
        {
            return new SubscriptionRegistrationForMethod<TComponent, TEvent>(componentRegistration);
        }
    }

    public class SubscriptionRegistrationForMethod<TComponent, TEvent>
    {
        private readonly ComponentRegistration<TComponent> componentRegistration;

        public SubscriptionRegistrationForMethod(ComponentRegistration<TComponent> componentRegistration)
        {
            this.componentRegistration = componentRegistration;
        }

        public ComponentRegistration<TComponent> WithMethod(Func<TComponent, TEvent> handlerProvider)
        {
            componentRegistration.ExtendedProperties(Property
                .ForKey(EventSubscriptionFacility.SubscriptionPropertyKey)
                .Eq(new EventSubscriptionFacilityConfig(typeof(TEvent), handlerProvider)));
            return componentRegistration;
        }
    }

    public class EventSubscriptionFacilityConfig
    {
        public Type EventType { get; private set; }
        public Delegate HandlerProvider { get; private set; }

        public EventSubscriptionFacilityConfig(Type eventType, Delegate handlerProvider)
        {
            EventType = eventType;
            HandlerProvider = handlerProvider;
        }
    }
}