using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Mike.AdvancedWindsorTricks.ComponentRelease;

namespace Mike.AdvancedWindsorTricks
{
    public class SubContainerSpike
    {
        public void SubContainer_should_get_components_of_parent()
        {
            var parentContainer = new WindsorContainer()
                .Register(
                    Component.For<Root>().LifeStyle.Transient,
                    Component.For<ChildNode>().LifeStyle.Transient,
                    Component.For<IGrandChild>().ImplementedBy<GrandChild>().LifeStyle.Transient
                );

            var subContainer1 = new WindsorContainer()
                .Register(
                    Component.For<IGrandChild>().ImplementedBy<FirstGrandChild>().LifeStyle.Transient
                );

            var subContainer2 = new WindsorContainer()
                .Register(
                    Component.For<IGrandChild>().ImplementedBy<SecondGrandChild>().LifeStyle.Transient
                );


            parentContainer.AddChildContainer(subContainer1);
            parentContainer.AddChildContainer(subContainer2);

            var parentRoot = parentContainer.Resolve<Root>();
            parentRoot.Accept(node => Console.WriteLine("Resolved from parent container: {0}", node.GetType().Name));

            var child1Root = subContainer1.Resolve<Root>();
            child1Root.Accept(node => Console.WriteLine("Resolved from subContainer1: {0}", node.GetType().Name));

            var child2Root = subContainer2.Resolve<Root>();
            child2Root.Accept(node => Console.WriteLine("Resolved from subContainer2: {0}", node.GetType().Name));
        }
    }

    public class FirstGrandChild : IGrandChild
    {
        public void Accept(GraphVisitor visitor)
        {
            visitor(this);
        }
    }

    public class SecondGrandChild : IGrandChild
    {
        public void Accept(GraphVisitor visitor)
        {
            visitor(this);
        }
    }
}