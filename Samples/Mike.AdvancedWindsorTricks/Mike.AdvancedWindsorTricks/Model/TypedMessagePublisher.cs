using System;

namespace Mike.AdvancedWindsorTricks.Model
{
    public class TypedMessagePublisher : IStartable
    {
        public event Action<NewCustomerEvent> NewCustomer;

        public void CreateNewCustomer(string name, int age)
        {
            if (NewCustomer != null)
            {
                NewCustomer(new NewCustomerEvent { Name = name, Age = age });
            }
        }

        public void Start(){}
        public void Stop(){}
    }

    public class NewCustomerSubscriber
    {
        public bool NewCustomerWasHandled { get; private set; }
        public string CustomerName { get; private set; }
        public int CustomerAge { get; private set; }

        public void Handle(NewCustomerEvent newCustomerEvent)
        {
            NewCustomerWasHandled = true;
            CustomerName = newCustomerEvent.Name;
            CustomerAge = newCustomerEvent.Age;
        }

        public AlternativeEvent AlternativeEvent { get; private set; }

        public void HandleAlternative(AlternativeEvent alternativeEvent)
        {
            AlternativeEvent = alternativeEvent;
        }
    }

    public class NewCustomerEvent
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class AlternativeEvent {}

    public class AlternativeEventPublisher : IStartable
    {
        public event Action<AlternativeEvent> AlternativeEvent;

        public void RaiseAlternativeEvent(AlternativeEvent alternativeEvent)
        {
            if (AlternativeEvent != null)
            {
                AlternativeEvent(alternativeEvent);
            }
        }

        public void Start(){}
        public void Stop(){}
    }
}