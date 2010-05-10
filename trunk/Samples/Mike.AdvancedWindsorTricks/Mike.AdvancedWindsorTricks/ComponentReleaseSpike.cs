using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Mike.AdvancedWindsorTricks.ComponentRelease
{
    public class ComponentReleaseSpike
    {
        public void Components_should_be_released()
        {
            var container = new WindsorContainer()
                .Register(
                    Component.For<Root>().LifeStyle.Transient,
                    Component.For<ChildNode>().LifeStyle.Transient,
                    Component.For<IGrandChild>().ImplementedBy<DisposableGrandChild>().LifeStyle.Transient
                );

            var memoryAtStart = GC.GetTotalMemory(true);
            Console.WriteLine("Memory at start:       {0}", memoryAtStart);

            var root = container.Resolve<Root>();
            root.Accept(node => Console.WriteLine("Hello from {0}", node.GetType().Name));

            var memoryAfterCreation = GC.GetTotalMemory(true);
            Console.WriteLine("Memory added after creation: {0}", memoryAfterCreation - memoryAtStart);

            container.Release(root);
            root = null;

            var memoryAfterRelease = GC.GetTotalMemory(true);
            Console.WriteLine("Memory added after release: {0}", memoryAfterRelease - memoryAtStart);
        }

        public void ByteSpike()
        {
            var memoryAtStart = GC.GetTotalMemory(true);
            Console.WriteLine("Memory at start:       {0}", memoryAtStart);

            var bytes = new byte[1000000];

            var memoryAfterCreation = GC.GetTotalMemory(true);
            Console.WriteLine("Memory added after creation: {0}", memoryAfterCreation - memoryAtStart);

            bytes = null;

            var memoryAfterRelease = GC.GetTotalMemory(true);
            Console.WriteLine("Memory added after release: {0}", memoryAfterRelease - memoryAtStart);
        }
    }



    public class Root : IGraphNode
    {
        private readonly ChildNode childNode;
        private byte[] bytes = new byte[1024 * 1024];

        public Root(ChildNode childNode)
        {
            this.childNode = childNode;
        }

        public void Accept(GraphVisitor visitor)
        {
            visitor(this);
            childNode.Accept(visitor);
        }
    }

    public class ChildNode : IGraphNode
    {
        private readonly IGrandChild grandChild;
        private byte[] bytes = new byte[1024 * 1024];

        public ChildNode(IGrandChild grandChild)
        {
            this.grandChild = grandChild;
        }

        public void Accept(GraphVisitor visitor)
        {
            visitor(this);
            grandChild.Accept(visitor);
        }
    }

    public interface IGrandChild : IGraphNode {}

    public class GrandChild : IGrandChild
    {
        private byte[] bytes = new byte[1024 * 1024];
        public void Accept(GraphVisitor visitor)
        {
            visitor(this);
        }
    }

    public class DisposableGrandChild : IGrandChild, IDisposable
    {
        private byte[] bytes = new byte[1024 * 1024];
        public void Accept(GraphVisitor visitor)
        {
            visitor(this);
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose called on DisposableGrandChild");
        }
    }

    public interface IGraphNode
    {
        void Accept(GraphVisitor visitor);
    }

    public delegate void GraphVisitor(IGraphNode node);
}